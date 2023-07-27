using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Repositories;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService
{
    public class MaterialService : IMaterialService
    {
        private IGenericRepository<Material> repo;

        public MaterialService()
        {
            this.repo = new MaterialRepository(new DbContext());
        }

        public IEnumerable<Material> GetMaterials()
        {
            return repo.GetItems();
        }

        public Material GetMaterialById(string materialId)
        {
            Guid materialGuid = Guid.Parse(materialId);
            return repo.GetItemByID(materialGuid);
        }

        public void AddMaterial(Material material)
        {
            Guid newId = new Guid();
            material.ID = newId;
            repo.InsertItem(material);
        }

        public void UpdateMaterial(string id, Material material)
        {
            Guid materialGuid = Guid.Parse(id);
            repo.UpdateItem(material);
        }

        public void DeleteMaterial(string id)
        {
            Guid materialId = Guid.Parse(id);
            repo.DeleteItem(materialId);
        }
    }
}
