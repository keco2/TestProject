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
        private string message = "TEST";
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
            set { SetProperty(ref selectedTask, value); }
        }

        public ICommand DeleteTaskCmd { get; private set; }

        public MainVM()
        {
            HookUpUICommands();
            LoadData();
        }

        private void LoadData()
        {
            var proxy = new Proxy();
            TaskList = proxy.GetTasks().ToObservableCollection();
            Message = "Records found: " + TaskList.Count;
        }

        private void HookUpUICommands()
        {
            DeleteTaskCmd = new DelegateCommand(_ => DeleteRecord(), _ => true); // SelectedTask != null);
        }

        public void DeleteRecord()
        {
            if (SelectedTask != null)
            {
                var taskName = SelectedTask.Name;
                var proxy = new Proxy();
                proxy.DeleteTask(SelectedTask.ID);
                var count = proxy.GetTasks().Count();
                LoadData();
                Message = taskName + " deleted / DB.count = " + count;
            }
            else
            {
                Message = "No task is selected";
            }
        }
    }
}
