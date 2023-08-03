﻿using System;
using System.Collections.Generic;
using TaskMgmt.DAL;
using TaskMgmt.Model;
using AutoMapper;
using System.Linq;
using TaskMgmt.WcfService.MappersConfigs;
using Unity;
using Unity.Wcf;
using Unity.Lifetime;

namespace TaskMgmt.WcfService
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            throw new Exception("WooooooooooooooooooW");

            // configure container
            container
                //.RegisterType<IService1, Service1>()
                //.RegisterType<IRespository<Blah>, BlahRepository>()
                //.RegisterType<IBlahContext, BlahContext>(new HierarchicalLifetimeManager());
                .RegisterType<ITaskService, TaskService>()
                .RegisterType<IUnitOfWork, UnitOfWorkRepository>(new HierarchicalLifetimeManager());
        }
    }



    public class TaskService : ITaskService
    {
        private IMapper mapper;

        [Dependency]
        public IUnitOfWork UnitOfWorkRepo { get; set; }

        public TaskService()
        {
            // DRAFT
            //UnitOfWorkRepo = new UnitOfWorkRepository();
            //

            mapper = new Mapper(new TaskMapperConfig());
        }

        public IEnumerable<Task> GetTasks()
        {
            var tasks = UnitOfWorkRepo.TaskRepository.GetItems();
            return mapper.Map<IEnumerable<Task>>(tasks);
        }

        public Task GetTaskById(string taskId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            var task = UnitOfWorkRepo.TaskRepository.GetItemsByID(taskGuid).Single();
            return mapper.Map<Task>(task);
        }

        public void AddTask(Task task)
        {
            UnitOfWorkRepo.TaskRepository.InsertItem(mapper.Map<TaskEntity>(task));
            UnitOfWorkRepo.SaveChanges();
        }

        public void UpdateTask(string id, Task task)
        {
            UnitOfWorkRepo.TaskRepository.UpdateItem(mapper.Map<TaskEntity>(task));
            UnitOfWorkRepo.SaveChanges();
        }

        public void DeleteTask(string id)
        {
            Guid taskGuid = Guid.Parse(id);
            UnitOfWorkRepo.TaskRepository.DeleteItem(taskGuid);
            UnitOfWorkRepo.SaveChanges();
        }
    }
}
