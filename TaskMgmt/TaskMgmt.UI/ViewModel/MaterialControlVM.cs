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
    public class MaterialControlVM : ViewModelBase, IMaterialVM, ILoadble
    {
        private readonly IProxy _proxy;

        public MaterialControlVM(IProxy proxy)
        {
            _proxy = proxy;
            HookUpUICommands();
            LoadMaterials();
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private IEnumerable<Material> materiallist;
        public IEnumerable<Material> MaterialList
        {
            get => materiallist;
            set => SetProperty(ref materiallist, value);
        }

        private Material selectedMaterial;
        public Material SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                SetProperty(ref selectedMaterial, value);
                UnitVariationOptions = _unitVariation.GetBaseUnits();
                SelectedUnit = selectedMaterial.UnitOfIssue?.Value;
                IsRecordChanged = false;
                IsRecordNew = false;
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

        private string[] currentUnitvariations;
        public string[] UnitVariationOptions
        {
            get => currentUnitvariations;
            set => SetProperty(ref currentUnitvariations, value);
        }

        private readonly UnitVariation _unitVariation = new UnitVariation();

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

        public ICommand NewMaterialCmd { get; private set; }
        public IAsyncCommand AddMaterialCmd { get; private set; }
        public IAsyncCommand UpdateMaterialCmd { get; private set; }
        public IAsyncCommand DeleteMaterialCmd { get; private set; }
        public ICommand RecordChangedCmd { get; private set; }

        public void InitLoad()
        {
            LoadMaterials();
        }

        public void LoadMaterials()
        {
            try
            {
                MaterialList = _proxy.GetMaterials();
                Message = "Records found: " + MaterialList.Count();
            }
            catch (EndpointNotFoundException)
            {
                Message = "ERROR: EndpointNotFoundException";
            }
        }

        private void HookUpUICommands()
        {
            NewMaterialCmd = new DelegateCommand(_ => PrepareNewRecord());
            AddMaterialCmd = new AsyncCommand(async () => await InvokeOnSelectedRecordAsync(AddRecord));
            UpdateMaterialCmd = new AsyncCommand(async () => await InvokeOnSelectedRecordAsync(UpdateRecord));
            DeleteMaterialCmd = new AsyncCommand(async () => await InvokeOnSelectedRecordAsync(DeleteRecord));
            RecordChangedCmd = new DelegateCommand(_ => IsRecordChanged = !IsRecordNew);
        }

        private async Async.Task InvokeOnSelectedRecordAsync(Func<IProxy, Async.Task> actionAsync)
        {
            if (SelectedMaterial != null)
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
                Message = "No material is selected";
            }
        }

        private void PrepareNewRecord()
        {
            SelectedMaterial = new Material();
            UnitVariationOptions = _unitVariation.GetBaseUnits();
            IsRecordNew = true;
        }

        private async Async.Task AddRecord(IProxy proxy)
        {
            if (IsRecordNew && SelectedMaterial != null && !MaterialList.Contains(SelectedMaterial) && GetDataValidation())
            {
                var newMaterialId = SelectedMaterial.ID;
                SelectedMaterial.UnitOfIssue = new Unit(SelectedUnit);
                await proxy.AddMaterialAsync(SelectedMaterial);
                LoadMaterials();
                SelectedMaterial = MaterialList.Single(t => t.ID == newMaterialId);
                Message = "Material " + SelectedMaterial.Partnumber + " added";
                IsRecordNew = false;
            }
        }

        private bool GetDataValidation()
        {
            if (String.IsNullOrEmpty(SelectedUnit))
            {
                Message = "Please select a unit";
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Async.Task UpdateRecord(IProxy proxy)
        {
            if (IsRecordChanged && GetDataValidation())
            {
                SelectedMaterial.UnitOfIssue.Value = SelectedUnit;
                var partnumber = SelectedMaterial.Partnumber;
                await proxy.UpdateMaterialAsync(SelectedMaterial.ID, SelectedMaterial);
                //LoadMaterials();
                Message = SelectedMaterial.Partnumber + " updated";
            }
            else
            {
                Message = "No change found";
            }
        }

        private async Async.Task DeleteRecord(IProxy proxy)
        {
            var materialName = SelectedMaterial.Partnumber;
            await proxy.DeleteMaterialAsync(SelectedMaterial.ID);
            LoadMaterials();
            Message = materialName + " deleted (related task-usages deleted)";
        }
    }
}
