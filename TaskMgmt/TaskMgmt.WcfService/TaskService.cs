using System;
using System.Collections.Generic;
using TaskMgmt.DAL;
using TaskMgmt.Model;
using AutoMapper;
using System.Linq;

namespace TaskMgmt.WcfService
{
    public class TaskService : ITaskService
    {
        private IUnitOfWork unitOfWorkRepo;
        private IMapper mapper;

        public TaskService()
        {
            unitOfWorkRepo = new UnitOfWorkRepository();
            IConfigurationProvider mapperCfg = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Task, TaskEntity>();
                    cfg.CreateMap<TaskEntity, Task>();
                });
            mapper = new Mapper(mapperCfg);
        }

        public IEnumerable<Task> GetTasks()
        {
            var tasks = unitOfWorkRepo.TaskRepository.GetItems();
            return mapper.Map<IEnumerable<Task>>(tasks);
        }

        public Task GetTaskById(string taskId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            var task = unitOfWorkRepo.TaskRepository.GetItemsByID(taskGuid).Single();
            return mapper.Map<Task>(task);
        }

        public void AddTask(Task task)
        {
            unitOfWorkRepo.TaskRepository.InsertItem(mapper.Map<TaskEntity>(task));
            unitOfWorkRepo.SaveChanges();
        }

        public void UpdateTask(string id, Task task)
        {
            unitOfWorkRepo.TaskRepository.UpdateItem(mapper.Map<TaskEntity>(task));
            unitOfWorkRepo.SaveChanges();
        }

        public void DeleteTask(string id)
        {
            Guid taskGuid = Guid.Parse(id);
            unitOfWorkRepo.TaskRepository.DeleteItem(taskGuid);
            unitOfWorkRepo.SaveChanges();
        }
    }
}
