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
using TaskMgmt.UI.ServiceRef;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskMgmt.UI.ViewHelper;

namespace TaskMgmt.UI.ViewModel
{
    class MainVM : ViewModelBase
    {
        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private ObservableCollection<Task> tasklist;
        public ObservableCollection<Task> TaskList
        {
            get => tasklist;
            set => SetProperty(ref tasklist, value);
        }

        private Task selectedTask;

        public Task SelectedTask
        {
            get { return selectedTask; }
            set {
                SetProperty(ref selectedTask, value);
                RecordChanged = false;
            }
        }

        private bool recordChanged = false;
        public bool RecordChanged
        {
            get => recordChanged;
            set => SetProperty(ref recordChanged, value);
        }

        public ICommand UpdateTaskCmd { get; private set; }
        public ICommand DeleteTaskCmd { get; private set; }
        public ICommand RecordChangedCmd { get; private set; }

        public MainVM()
        {
            HookUpUICommands();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var proxy = new Proxy();
                TaskList = proxy.GetTasks().ToObservableCollection();
                Message = "Records found: " + TaskList.Count;
            }
            catch (EndpointNotFoundException)
            {
                Message = "ERROR: EndpointNotFoundException";
            }
        }

        private void HookUpUICommands()
        {
            UpdateTaskCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(UpdateRecord));
            DeleteTaskCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(DeleteRecord));
            RecordChangedCmd = new DelegateCommand(_ => RecordChanged = true);
        }

        private void InvokeOnSelectedRecord(Action<Proxy> action)
        {
            if (SelectedTask != null)
            {
                var proxy = new Proxy();
                action.Invoke(proxy);
            }
            else
            {
                Message = "No task is selected";
            }
        }

        private void UpdateRecord(Proxy proxy)
        {
            if (RecordChanged)
            {
                proxy.UpdateTask(SelectedTask.ID, SelectedTask);
                Message = SelectedTask.Name + " updated";
            }
            else
            {
                Message = "No change found";
            }
        }

        public void DeleteRecord(Proxy proxy)
        {
            var taskName = SelectedTask.Name;
            proxy.DeleteTask(SelectedTask.ID);
            var count = proxy.GetTasks().Count();
            LoadData();
            Message = taskName + " deleted / DB.count = " + count;
        }
    }
}
