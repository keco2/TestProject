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
                SelectedUnit = null;
                LoadUsages();
            }
        }

        private Material selectedMaterial;
        public Material SelectedMaterial
        {
            get { return selectedMaterial; }
            set {
                SetProperty(ref selectedMaterial, value);
                UnitVariationOptions = _unitVariation.GetVariations(selectedMaterial.UniteOfIssue);
                Message = "";
            }
        }

        private TaskMaterialUsage selectedTaskmaterialusage;
        public TaskMaterialUsage SelectedTaskMaterialUsage
        {
            get { return selectedTaskmaterialusage; }
            set
            {
                SetProperty(ref selectedTaskmaterialusage, value);
                UnitVariationOptions = _unitVariation.GetVariations(selectedTaskmaterialusage.Material?.UniteOfIssue);
                SelectedUnit = selectedTaskmaterialusage.UniteOfMeasurement.Value;
                IsRecordChanged = false;
                IsRecordNew = false;
                Message = "";
            }
        }

        private string selectedUnitvariation;
        public string SelectedUnit
        {
            get => selectedUnitvariation;
            set
            {
                SetProperty(ref selectedUnitvariation, value);
                IsRecordChanged = true;
                Message = "";
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

        private string[] baseunits;
        public string[] BaseUnits
        {
            get => baseunits;
            set => SetProperty(ref baseunits, value);
        }

        private string[] currentUnitvariations;
        public string[] UnitVariationOptions
        {
            get => currentUnitvariations;
            set => SetProperty(ref currentUnitvariations, value);
        }

        private readonly UnitVariation _unitVariation = new UnitVariation();

        public ICommand NewTaskCmd { get; private set; }
        public ICommand AddTaskCmd { get; private set; }
        public ICommand UpdateTaskCmd { get; private set; }
        public ICommand DeleteTaskCmd { get; private set; }
        public ICommand RecordChangedCmd { get; private set; }
        public ICommand NewUsageCmd { get; private set; }
        public ICommand AddUsageCmd { get; private set; }
        public ICommand UpdateUsageCmd { get; private set; }

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
            UpdateUsageCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(UpdateUsage));
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
                if (SelectedTaskMaterialUsage == null ||  SelectedMaterial == null || String.IsNullOrEmpty(SelectedTaskMaterialUsage.UniteOfMeasurement.Value))
                {
                    Message = "Missing information";
                }
                else
                {
                    SelectedTaskMaterialUsage.Task = SelectedTask;
                    SelectedTaskMaterialUsage.Material = SelectedMaterial;
                    var taskId = SelectedTaskMaterialUsage.Task.ID;
                    var materialId = SelectedTaskMaterialUsage.Material.ID;
                    //SelectedTaskMaterialUsage.UniteOfMeasurement = CurrentUnitVariations.
                    proxy.AddUsage(SelectedTaskMaterialUsage);
                    LoadUsages();
                    SelectedTaskMaterialUsage = TaskMaterialUsageList.Single(u => u.Task.ID == taskId && u.Material.ID == materialId);
                    Message = "Material " + SelectedTaskMaterialUsage.Material.ManufacturerCode + " assigned to task " + SelectedTaskMaterialUsage.Task.Name;
                    IsRecordNew = false;
                    isRecordChanged = false;
                }
            }
        }

        private void UpdateUsage(Proxy proxy)
        {
            if (IsRecordChanged)
            {
                SelectedTaskMaterialUsage.UniteOfMeasurement.Value = SelectedUnit;
                proxy.UpdateUsage(SelectedTaskMaterialUsage);
                LoadUsages();
                Message = "Usage on " + SelectedTask.Name + " updated";
            }
            else
            {
                Message = "No change found";
            }
        }
    }
}
