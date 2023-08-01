using System;
using System.Collections.Generic;
using System.Linq;
using TaskMgmt.DAL;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService
{
    public class MaterialService : IMaterialService
    {
        private IUnitOfWork unitOfWorkRepo;

        public MaterialService()
        {
            unitOfWorkRepo = new UnitOfWorkRepository();
        }

        public IEnumerable<Material> GetMaterials()
        {
            return unitOfWorkRepo.MaterialRepository.GetItems();
        }

        public Material GetMaterialById(string materialId)
        {
            Guid materialGuid = Guid.Parse(materialId);
            return unitOfWorkRepo.MaterialRepository.GetItemsByID(materialGuid).Single();
        }

        public void AddMaterial(Material material)
        {
            Guid newId = new Guid();
            material.ID = newId;
            unitOfWorkRepo.MaterialRepository.InsertItem(material);
        }

        public void UpdateMaterial(string id, Material material)
        {
            Guid materialGuid = Guid.Parse(id);
            unitOfWorkRepo.MaterialRepository.UpdateItem(material);
        }

        public void DeleteMaterial(string id)
        {
            Guid materialId = Guid.Parse(id);
            unitOfWorkRepo.MaterialRepository.DeleteItem(materialId);
        }
    }
}
