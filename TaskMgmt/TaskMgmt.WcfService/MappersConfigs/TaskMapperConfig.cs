﻿using AutoMapper;
using TaskMgmt.DAL;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService.MappersConfigs
{
    public class TaskMapperConfig : MapperConfiguration
    {
        public TaskMapperConfig() : base(
                cfg =>
                {
                    cfg.CreateMap<Task, TaskEntity>();
                    cfg.CreateMap<TaskEntity, Task>();
                })
        {
            //
        }
    }
}
