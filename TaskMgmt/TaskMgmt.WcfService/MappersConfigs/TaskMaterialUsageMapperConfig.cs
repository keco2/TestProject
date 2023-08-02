using AutoMapper;
using System;
using TaskMgmt.DAL;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService.MappersConfigs
{
    public class TaskMaterialUsageMapperConfig : MapperConfiguration
    {
        public TaskMaterialUsageMapperConfig() : base(
                cfg =>
                {
                    cfg.CreateMap<Task, TaskEntity>();
                    cfg.CreateMap<TaskEntity, Task>();

                    cfg.CreateMap<Material, MaterialEntity>()
                    .ForMember(dest => dest.UnitOfIssue, options => options.MapFrom(src => src.UnitOfIssue.Value));
                    cfg.CreateMap<MaterialEntity, Material>()
                    .ForMember(dest => dest.UnitOfIssue, options => options.MapFrom(src => new Unit(src.UnitOfIssue)));

                    cfg.CreateMap<TaskMaterialUsage, TaskMaterialUsageEntity>()
                    .ForMember(dest => dest.UnitOfMeasurement, options => options.MapFrom(src => src.UnitOfMeasurement.Value))
                    .ForMember(dest => dest.MaterialID, options => options.MapFrom(src => src.Material.ID))
                    .ForMember(dest => dest.TaskID, options => options.MapFrom(src => src.Task.ID))
                    .ForMember(dest => dest.Material, options => options.Ignore())
                    .ForMember(dest => dest.Task, options => options.Ignore());
                    cfg.CreateMap<TaskMaterialUsageEntity, TaskMaterialUsage>().ForMember(dest => dest.UnitOfMeasurement, options
                        => options.MapFrom(src => new Unit(src.UnitOfMeasurement)));
                })
        {
            //
        }
    }
}
