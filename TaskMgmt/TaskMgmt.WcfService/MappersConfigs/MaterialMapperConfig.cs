using AutoMapper;
using TaskMgmt.DAL;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService.MappersConfigs
{
    public class MaterialMapperConfig : MapperConfiguration
    {
        public MaterialMapperConfig() : base(
                cfg =>
                {
                    cfg.CreateMap<Material, MaterialEntity>()
                    .ForMember(dest => dest.UnitOfIssue, options => options.MapFrom(src => src.UnitOfIssue.Value));
                    cfg.CreateMap<MaterialEntity, Material>()
                    .ForMember(dest => dest.UnitOfIssue, options => options.MapFrom(src => new Unit(src.UnitOfIssue)));
                })
        {
            //
        }
    }
}
