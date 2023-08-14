using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Windows.Input;
using TaskMgmt.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskMgmt.UI.ViewHelper;
using TaskMgmt.BLL;
using Async = System.Threading.Tasks;

namespace TaskMgmt.UI.ViewModel
{
    public class TaskControlVM : ViewModelBase, ITaskVM, ILoadble
    {
        private readonly IProxy _proxy;

        public TaskControlVM(IProxy proxy)
        {
            _proxy = proxy;
            HookUpUICommands();
            LoadTasks();
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private IEnumerable<Task> tasklist;
        public IEnumerable<Task> TaskList
        {
            get => tasklist;
            set => SetProperty(ref tasklist, value);
        }

        private Task selectedTask;
        public Task SelectedTask
        {
            get { return selectedTask; }
            set
            {
                SetProperty(ref selectedTask, value);
                IsRecordChanged = false;
                IsRecordNew = false;
            }
        }

        private bool isRecordNew = false;
        public bool IsRecordNew
        {
            get => isRecordNew;
            set => SetProperty(ref isRecordNew, value);
        }

        private bool isRecordChanged = false;

        public bool IsRecordChanged
        {
            get => isRecordChanged;
            set => SetProperty(ref isRecordChanged, value);
        }

        public ICommand NewTaskCmd { get; private set; }
        public IAsyncCommand AddTaskCmd { get; private set; }
        public IAsyncCommand UpdateTaskCmd { get; private set; }
        public IAsyncCommand DeleteTaskCmd { get; private set; }
        public ICommand RecordChangedCmd { get; private set; }

        public void InitLoad()
        {
            LoadTasks();
        }

        public void LoadTasks()
        {
            try
            {
                TaskList = _proxy.GetTasks();
                Message = "Records found: " + TaskList.Count();
            }
            catch (EndpointNotFoundException)
            {
                Message = $"ERROR: {nameof(EndpointNotFoundException)}";
            }
            catch (CommunicationObjectFaultedException)
            {
                Message = $"ERROR: {nameof(CommunicationObjectFaultedException)}";
            }
        }

        private void HookUpUICommands()
        {
            NewTaskCmd = new DelegateCommand(_ => PrepareNewRecord());
            AddTaskCmd = new AsyncCommand(async () => await InvokeOnSelectedRecordAsync(AddRecord));
            UpdateTaskCmd = new AsyncCommand(async () => await InvokeOnSelectedRecordAsync(UpdateRecord));
            DeleteTaskCmd = new AsyncCommand(async () => await InvokeOnSelectedRecordAsync(DeleteRecord));
            RecordChangedCmd = new DelegateCommand(_ => IsRecordChanged = !IsRecordNew);
        }

        private async Async.Task InvokeOnSelectedRecordAsync(Func<IProxy, Async.Task> actionASync)
        {
            if (SelectedTask != null)
            {
                try
                {
                    await actionASync.Invoke(_proxy);
                }
                catch (InvalidCastException)
                {
                    Message = "Invalid data";
                }
                catch (EndpointNotFoundException)
                {
                    Message = "ERROR: EndpointNotFoundException";
                }
            }
            else
            {
                Message = "No task is selected";
            }
        }

        private void PrepareNewRecord()
        {
            SelectedTask = new Task();
            IsRecordNew = true;
        }

        private async Async.Task AddRecord(IProxy proxy)
        {
            if (IsRecordNew && SelectedTask != null && !TaskList.Contains(SelectedTask))
            {
                ValidateData(SelectedTask);
                var newtaskId = SelectedTask.ID;
                await proxy.AddTaskAsync(SelectedTask);
                LoadTasks();
                SelectedTask = TaskList.Single(t => t.ID == newtaskId);
                Message = "Task " + SelectedTask.Name + " added";
                IsRecordNew = false;
            }
        }

        private void ValidateData(Object selectedTask)
        {
            // Todo: ValidateData on Add/Update
            // throw new InvalidCastException();
        }

        private async Async.Task UpdateRecord(IProxy proxy)
        {
            if (IsRecordChanged)
            {
                ValidateData(SelectedTask);
                await proxy.UpdateTaskAsync(SelectedTask.ID, SelectedTask);
                Message = SelectedTask.Name + " updated";
            }
            else
            {
                Message = "No change found";
            }
        }

        private async Async.Task DeleteRecord(IProxy proxy)
        {
            var taskName = SelectedTask.Name;
            await proxy.DeleteTaskAsync(SelectedTask.ID);
            LoadTasks();
            Message = taskName + " deleted (related material-usages deleted)";
        }
    }
}
