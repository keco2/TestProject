using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Interface;
using TaskMgmt.Model;
using TaskMgmt.WcfService.MappersConfigs;
using Unity;
using Async = System.Threading.Tasks;

namespace TaskMgmt.WcfService
{
    public class MaterialService : IMaterialService
    {
        [Dependency]
        public IUnitOfWork UnitOfWorkRepo { get; set; }

        private IMapper mapper;

        public MaterialService()
        {
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

        public async Async.Task AddMaterialAsync(Material material)
        {
            var materialEntity = mapper.Map<MaterialEntity>(material);
            UnitOfWorkRepo.MaterialRepository.InsertItem(materialEntity);
            await UnitOfWorkRepo.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Async.Task UpdateMaterialAsync(string id, Material material)
        {
            Guid materialGuid = Guid.Parse(id);
            var materialEntity = mapper.Map<MaterialEntity>(material);
            UnitOfWorkRepo.MaterialRepository.UpdateItem(materialEntity);
            await UnitOfWorkRepo.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Async.Task DeleteMaterialAsync(string id)
        {
            Guid materialId = Guid.Parse(id);
            UnitOfWorkRepo.MaterialRepository.DeleteItem(materialId);
            await UnitOfWorkRepo.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
