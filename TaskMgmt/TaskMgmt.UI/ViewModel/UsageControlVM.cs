﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows.Input;
using TaskMgmt.BLL;
using TaskMgmt.Model;
using TaskMgmt.UI.ViewHelper;
using Async = System.Threading.Tasks;

namespace TaskMgmt.UI.ViewModel
{
    public class UsageControlVM : ViewModelBase, IUsageVM, ILoadble
    {
        private readonly IProxy _proxy;

        public UsageControlVM(IProxy proxy)
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
                SelectedUnit = null;
                LoadUsages();
                IsRecordChanged = false;
                IsRecordNew = false;
            }
        }

        private Material selectedMaterial;
        public Material SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                SetProperty(ref selectedMaterial, value);
                UnitVariationOptions = _unitVariation.GetVariations(selectedMaterial.UnitOfIssue);
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
                UnitVariationOptions = _unitVariation.GetVariations(selectedTaskmaterialusage.Material?.UnitOfIssue);
                SelectedUnit = selectedTaskmaterialusage.UnitOfMeasurement?.Value;
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
                IsRecordChanged = !IsRecordNew;
                Message = "";
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

        public ICommand RecordChangedCmd { get; private set; }
        public ICommand NewUsageCmd { get; private set; }
        public IAsyncCommand AddUsageCmd { get; private set; }
        public IAsyncCommand UpdateUsageCmd { get; private set; }
        public IAsyncCommand DeleteUsageCmd { get; private set; }

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
                Message = "ERROR: EndpointNotFoundException";
            }
        }

        private void LoadMaterial() => MaterialList = _proxy.GetMaterials();

        private void LoadUsages() => TaskMaterialUsageList = _proxy.GetUsagesByTaskId(SelectedTask.ID);

        private void HookUpUICommands()
        {
            RecordChangedCmd = new DelegateCommand(_ => IsRecordChanged = !IsRecordNew);
            NewUsageCmd = new DelegateCommand(async _ => await InvokeOnSelectedRecordAsync(PrepareNewUsage));
            AddUsageCmd = new AsyncCommand(async () => await InvokeOnSelectedRecordAsync(AddUsage));
            UpdateUsageCmd = new AsyncCommand(async () => await InvokeOnSelectedRecordAsync(UpdateUsage));
            DeleteUsageCmd = new AsyncCommand(async () => await InvokeOnSelectedRecordAsync(DeleteUsage));
        }

        private async Async.Task InvokeOnSelectedRecordAsync(Func<IProxy, Async.Task> actionAsync)
        {
            if (SelectedTask != null)
            {
                try
                {
                    await actionAsync.Invoke(_proxy);
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

        private void ValidateData(Object selectedTask)
        {
            // Todo: ValidateData on Add/Update
            // throw new InvalidCastException();
        }

        private async Async.Task PrepareNewUsage(IProxy proxy)
        {
            LoadMaterial();
            SelectedTaskMaterialUsage = new TaskMaterialUsage();
            IsRecordChanged = false;
            IsRecordNew = true;
        }

        private async Async.Task AddUsage(IProxy proxy)
        {
            if (IsRecordNew && !TaskMaterialUsageList.Contains(SelectedTaskMaterialUsage))
            {
                if (SelectedTaskMaterialUsage == null || SelectedUnit == null || SelectedMaterial == null || String.IsNullOrEmpty(SelectedUnit))
                {
                    Message = "Missing information";
                }
                else if (TaskMaterialUsageList.Any(u => u.Material.ID == SelectedMaterial.ID && u.Task.ID == SelectedTask.ID))
                {
                    Message = "Usage already exists - required behaviour not specified";
                }
                else
                {
                    ValidateData(SelectedTaskMaterialUsage);
                    SelectedTaskMaterialUsage.UnitOfMeasurement = new Unit(SelectedUnit);
                    SelectedTaskMaterialUsage.Task = SelectedTask;
                    SelectedTaskMaterialUsage.Material = SelectedMaterial;
                    var taskId = SelectedTaskMaterialUsage.Task.ID;
                    var materialId = SelectedTaskMaterialUsage.Material.ID;
                    await proxy.AddUsageAsync(SelectedTaskMaterialUsage);
                    LoadUsages();
                    SelectedTaskMaterialUsage = TaskMaterialUsageList.Single(u => u.Task.ID == taskId && u.Material.ID == materialId);
                    Message = "Material " + SelectedTaskMaterialUsage.Material.ManufacturerCode + " assigned to task " + SelectedTaskMaterialUsage.Task.Name;
                    IsRecordNew = false;
                    isRecordChanged = false;
                }
            }
        }

        private async Async.Task UpdateUsage(IProxy proxy)
        {
            if (IsRecordChanged)
            {
                ValidateData(SelectedTaskMaterialUsage);
                SelectedTaskMaterialUsage.UnitOfMeasurement.Value = SelectedUnit;
                await proxy.UpdateUsageAsync(SelectedTaskMaterialUsage);
                LoadUsages();
                Message = "Usage on " + SelectedTask.Name + " updated";
            }
            else
            {
                Message = "No change found";
            }
        }

        private async Async.Task DeleteUsage(IProxy proxy)
        {
            var taskName = SelectedTask.Name;
            await proxy.DeleteUsageAsync(SelectedTaskMaterialUsage.Task.ID, SelectedTaskMaterialUsage.Material.ID);
            LoadUsages();
            var count = TaskMaterialUsageList.Count();
            Message = "Usage from task " + taskName + " deleted / DB.count = " + count;
        }
    }
}
