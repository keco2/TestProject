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
    public class MaterialControlVM : ViewModelBase
    {
        private readonly Proxy _proxy;

        public MaterialControlVM()
        {
            HookUpUICommands();
            _proxy = new Proxy();
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
        public ICommand AddMaterialCmd { get; private set; }
        public ICommand UpdateMaterialCmd { get; private set; }
        public ICommand DeleteMaterialCmd { get; private set; }
        public ICommand RecordChangedCmd { get; private set; }

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
            AddMaterialCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(AddRecord));
            UpdateMaterialCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(UpdateRecord));
            DeleteMaterialCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(DeleteRecord));
            RecordChangedCmd = new DelegateCommand(_ => IsRecordChanged = !IsRecordNew);
        }

        private void InvokeOnSelectedRecord(Action<Proxy> action)
        {
            if (SelectedMaterial != null)
            {
                try
                {
                    action.Invoke(_proxy);
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

        private void AddRecord(Proxy proxy)
        {
            if (IsRecordNew && SelectedMaterial != null && !MaterialList.Contains(SelectedMaterial) && GetDataValidation())
            {
                var newMaterialId = SelectedMaterial.ID;
                SelectedMaterial.UnitOfIssue = new Unit(SelectedUnit);
                proxy.AddMaterial(SelectedMaterial);
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

        private void UpdateRecord(Proxy proxy)
        {
            if (IsRecordChanged && GetDataValidation())
            {
                SelectedMaterial.UnitOfIssue.Value = SelectedUnit;
                var partnumber = SelectedMaterial.Partnumber;
                proxy.UpdateMaterial(SelectedMaterial.ID, SelectedMaterial);
                //LoadMaterials();
                Message = SelectedMaterial.Partnumber + " updated";
            }
            else
            {
                Message = "No change found";
            }
        }

        private void DeleteRecord(Proxy proxy)
        {
            var materialName = SelectedMaterial.Partnumber;
            proxy.DeleteMaterial(SelectedMaterial.ID);
            LoadMaterials();
            Message = materialName + " deleted (related task-usages deleted)";
        }
    }
}
