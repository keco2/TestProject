using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskMgmt.DAL;
using TaskMgmt.Model;
using TaskMgmt.WcfService.MappersConfigs;
//using Unity.Container;

namespace TaskMgmt.WcfService
{
    public class MaterialService : IMaterialService
    {
        //[Dependency]
        public IUnitOfWork UnitOfWorkRepo { get; set; }

        private IMapper mapper;

        public MaterialService()
        {
            // DRAFT
            //UnitOfWorkRepo = new UnitOfWorkRepository();
            //

            mapper = new Mapper(new MaterialMapperConfig());
        }

        public IEnumerable<Material> GetMaterials()
        {
            var materials = UnitOfWorkRepo.MaterialRepository.GetItems();
            return mapper.Map<IEnumerable<Material>>(materials);
        }

        public Material GetMaterialById(string materialId)
        {
            Guid materialGuid = Guid.Parse(materialId);
            var task = UnitOfWorkRepo.MaterialRepository.GetItemsByID(materialGuid).Single();
            return mapper.Map<Material>(task);
        }

        public void AddMaterial(Material material)
        {
            var materialEntity = mapper.Map<MaterialEntity>(material);
            UnitOfWorkRepo.MaterialRepository.InsertItem(materialEntity);
            UnitOfWorkRepo.SaveChanges();
        }

        public void UpdateMaterial(string id, Material material)
        {
            Guid materialGuid = Guid.Parse(id);
            var materialEntity = mapper.Map<MaterialEntity>(material);
            UnitOfWorkRepo.MaterialRepository.UpdateItem(materialEntity);
            UnitOfWorkRepo.SaveChanges();
        }

        public void DeleteMaterial(string id)
        {
            Guid materialId = Guid.Parse(id);
            UnitOfWorkRepo.MaterialRepository.DeleteItem(materialId);
            UnitOfWorkRepo.SaveChanges();
        }
    }
}
