using System;
using System.Collections.Generic;
using TaskMgmt.DAL;
using TaskMgmt.Model;
using AutoMapper;
using System.Linq;
using TaskMgmt.WcfService.MappersConfigs;
using Unity;

namespace TaskMgmt.WcfService
{
    public class TaskService : ITaskService
    {
        private IMapper mapper;

        [Dependency]
        public IUnitOfWork UnitOfWorkRepo { get; set; }

        public TaskService()
        {
            // DRAFT
            UnitOfWorkRepo = new UnitOfWorkRepository();
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
