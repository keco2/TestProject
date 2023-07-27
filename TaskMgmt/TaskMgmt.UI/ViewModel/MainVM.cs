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

namespace TaskMgmt.UI.ViewModel
{
    class MainVM : ViewModelBase
    {
        private readonly Proxy _proxy;

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

        private IEnumerable<Material> materiallist;
        public IEnumerable<Material> MaterialList
        {
            get => materiallist;
            set => SetProperty(ref materiallist, value);
        }

        private IEnumerable<TaskMaterialUsage> taskmaterialUsageList;
        public IEnumerable<TaskMaterialUsage> TaskMaterialUsageList
        {
            get => taskmaterialUsageList;
            set => SetProperty(ref taskmaterialUsageList, value);
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
                LoadUsages();
            }
        }

        private Material selectedMaterial;
        public Material SelectedMaterial
        {
            get { return selectedMaterial; }
            set { SetProperty(ref selectedMaterial, value); }
        }

        private TaskMaterialUsage selectedTaskmaterialusage;
        public TaskMaterialUsage SelectedTaskMaterialUsage
        {
            get { return selectedTaskmaterialusage; }
            set
            {
                SetProperty(ref selectedTaskmaterialusage, value);
                IsRecordChanged = false;
                IsRecordNew = false;
            }
        }

        private void LoadMaterial() => MaterialList = _proxy.GetMaterials();

        private void LoadUsages() => TaskMaterialUsageList = _proxy.GetUsagesByTaskId(SelectedTask.ID);

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
        public ICommand AddTaskCmd { get; private set; }
        public ICommand UpdateTaskCmd { get; private set; }
        public ICommand DeleteTaskCmd { get; private set; }
        public ICommand RecordChangedCmd { get; private set; }
        public ICommand NewUsageCmd { get; private set; }
        public ICommand AddUsageCmd { get; private set; }

        public MainVM()
        {
            HookUpUICommands();
            _proxy = new Proxy();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                TaskList = _proxy.GetTasks();
                Message = "Records found: " + TaskList.Count();
            }
            catch (EndpointNotFoundException)
            {
                Message = "ERROR: EndpointNotFoundException";
            }
        }

        private void HookUpUICommands()
        {
            NewTaskCmd = new DelegateCommand(_ => PrepareNewRecord());
            AddTaskCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(AddRecord));
            UpdateTaskCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(UpdateRecord));
            DeleteTaskCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(DeleteRecord));
            RecordChangedCmd = new DelegateCommand(_ => IsRecordChanged = IsRecordNew ? false : true);

            NewUsageCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(PrepareNewUsage));
            AddUsageCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(AddUsage));
        }

        private void PrepareNewRecord()
        {
            SelectedTask = new Task();
            IsRecordNew = true;
        }

        private void InvokeOnSelectedRecord(Action<Proxy> action)
        {
            if (SelectedTask != null)
            {
                action.Invoke(_proxy);
            }
            else
            {
                Message = "No task is selected";
            }
        }

        private void AddRecord(Proxy proxy)
        {
            if (IsRecordNew && SelectedTask != null && !TaskList.Contains(SelectedTask))
            {
                var newtaskId = SelectedTask.ID;
                proxy.AddTask(SelectedTask);
                LoadData();
                SelectedTask = TaskList.Single(t => t.ID == newtaskId);
                Message = "Task " + SelectedTask.Name + " added";
                IsRecordNew = false;
            }
        }

        private void UpdateRecord(Proxy proxy)
        {
            if (IsRecordChanged)
            {
                proxy.UpdateTask(SelectedTask.ID, SelectedTask);
                Message = SelectedTask.Name + " updated";
            }
            else
            {
                Message = "No change found";
            }
        }

        private void DeleteRecord(Proxy proxy)
        {
            var taskName = SelectedTask.Name;
            proxy.DeleteTask(SelectedTask.ID);
            LoadData();
            var count = TaskList.Count();
            Message = taskName + " deleted / DB.count = " + count;
        }

        private void PrepareNewUsage(Proxy proxy)
        {
            LoadMaterial();
            SelectedTaskMaterialUsage = new TaskMaterialUsage();
            IsRecordChanged = false;
            IsRecordNew = true;
        }

        private void AddUsage(Proxy proxy)
        {
            if (IsRecordNew && !TaskMaterialUsageList.Contains(SelectedTaskMaterialUsage))
            {
                if (SelectedTaskMaterialUsage == null ||  SelectedMaterial == null || String.IsNullOrEmpty(SelectedTaskMaterialUsage.UniteOfMeasurement))
                {
                    Message = "Missing information";
                }
                else
                {
                    SelectedTaskMaterialUsage.Task = SelectedTask;
                    SelectedTaskMaterialUsage.Material = SelectedMaterial;
                    var taskId = SelectedTaskMaterialUsage.Task.ID;
                    var materialId = SelectedTaskMaterialUsage.Material.ID;
                    proxy.AddUsage(SelectedTaskMaterialUsage);
                    LoadUsages();
                    SelectedTaskMaterialUsage = TaskMaterialUsageList.Single(u => u.Task.ID == taskId && u.Material.ID == materialId);
                    Message = "Material " + SelectedTaskMaterialUsage.Material.ManufacturerCode + " assigned to task " + SelectedTaskMaterialUsage.Task.Name;
                    IsRecordNew = false;
                }
            }
        }
    }
}
