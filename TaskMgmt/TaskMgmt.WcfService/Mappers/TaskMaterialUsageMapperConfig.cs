using AutoMapper;
using TaskMgmt.DAL;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService
{
    public class TaskMaterialUsageMapperConfig : MapperConfiguration
    {
        public TaskMaterialUsageMapperConfig() : base(
                cfg =>
                {
                    IMapper taskMapper = new Mapper(new TaskMapperConfig());
                    IMapper materialMapper = new Mapper(new MaterialMapperConfig());

                    //----------------------------------------------------------------------------

                    cfg.CreateMap<Task, TaskEntity>();
                    cfg.CreateMap<TaskEntity, Task>();

                    cfg.CreateMap<Material, MaterialEntity>()
                    .ForMember(dest => dest.UnitOfIssue, options => options.MapFrom(src => src.UnitOfIssue.Value));
                    cfg.CreateMap<MaterialEntity, Material>()
                    .ForMember(dest => dest.UnitOfIssue, options => options.MapFrom(src => new Unit(src.UnitOfIssue)));

                    //----------------------------------------------------------------------------

                    cfg.CreateMap<TaskMaterialUsage, TaskMaterialUsageEntity>()
                    .ForMember(dest => dest.UnitOfMeasurement, options => options.MapFrom(src => src.UnitOfMeasurement.Value))
                    //.ForMember(dest => dest.Material, options => options.MapFrom(src => materialMapper.Map<MaterialEntity>(src.Material)))
                    //.ForMember(dest => dest.Task, options => options.MapFrom(src => taskMapper.Map<TaskEntity>(src.Task)))
                    .ForMember(dest => dest.MaterialID, options => options.MapFrom(src => src.Material.ID))
                    .ForMember(dest => dest.TaskID, options => options.MapFrom(src => src.Task.ID));

                    cfg.CreateMap<TaskMaterialUsageEntity, TaskMaterialUsage>()
                    .ForMember(dest => dest.UnitOfMeasurement, options => options.MapFrom(src => new Unit(src.UnitOfMeasurement)))
                    //.ForMember(dest => dest.Material, options => options.MapFrom(src => materialMapper.Map<Material>(src.Material)))
                    //.ForMember(dest => dest.Task, options => options.MapFrom(src => taskMapper.Map<Task>(src.Task)))
                    ;
                })
        {
            //
        }
    }
}
