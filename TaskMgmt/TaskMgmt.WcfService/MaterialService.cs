using AutoMapper;
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
        private IMapper mapper;

        public MaterialService()
        {
            unitOfWorkRepo = new UnitOfWorkRepository();
            IConfigurationProvider mapperCfg = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Material, MaterialEntity>().ForMember(dest => dest.UnitOfIssue,  options => options.MapFrom(src => src.UnitOfIssue.Value));
                    cfg.CreateMap<MaterialEntity, Material>().ForMember(dest => dest.UnitOfIssue, options => options.MapFrom(src => new Unit(src.UnitOfIssue)));
                });
            mapper = new Mapper(mapperCfg);
        }

        public IEnumerable<Material> GetMaterials()
        {
            var materials = unitOfWorkRepo.MaterialRepository.GetItems();
            return mapper.Map<IEnumerable<Material>>(materials);
        }

        public Material GetMaterialById(string materialId)
        {
            Guid materialGuid = Guid.Parse(materialId);
            var task = unitOfWorkRepo.MaterialRepository.GetItemsByID(materialGuid).Single();
            return mapper.Map<Material>(task);
        }

        public void AddMaterial(Material material)
        {
            var materialEntity = mapper.Map<MaterialEntity>(material);
            unitOfWorkRepo.MaterialRepository.InsertItem(materialEntity);
            unitOfWorkRepo.SaveChanges();
        }

        public void UpdateMaterial(string id, Material material)
        {
            Guid materialGuid = Guid.Parse(id);
            var materialEntity = mapper.Map<MaterialEntity>(material);
            unitOfWorkRepo.MaterialRepository.UpdateItem(materialEntity);
            unitOfWorkRepo.SaveChanges();
        }

        public void DeleteMaterial(string id)
        {
            Guid materialId = Guid.Parse(id);
            unitOfWorkRepo.MaterialRepository.DeleteItem(materialId);
            unitOfWorkRepo.SaveChanges();
        }
    }
}
