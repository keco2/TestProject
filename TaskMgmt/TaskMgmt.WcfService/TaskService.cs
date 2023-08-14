using System;
using System.Collections.Generic;
using TaskMgmt.DAL;
using TaskMgmt.Model;
using AutoMapper;
using System.Linq;
using TaskMgmt.WcfService.MappersConfigs;
using Unity;
using TaskMgmt.DAL.Interface;
using Async = System.Threading.Tasks;

namespace TaskMgmt.WcfService
{
    public class TaskService : ITaskService
    {
        private IMapper mapper;

        [Dependency]
        public IUnitOfWork UnitOfWorkRepo { get; set; }

        public TaskService()
        {
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

        public async Async.Task AddTaskAsync(Task task)
        {
            UnitOfWorkRepo.TaskRepository.InsertItem(mapper.Map<TaskEntity>(task));
            await UnitOfWorkRepo.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Async.Task UpdateTaskAsync(string id, Task task)
        {
            UnitOfWorkRepo.TaskRepository.UpdateItem(mapper.Map<TaskEntity>(task));
            await UnitOfWorkRepo.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Async.Task DeleteTaskAsync(string id)
        {
            Guid taskGuid = Guid.Parse(id);
            UnitOfWorkRepo.TaskRepository.DeleteItem(taskGuid);
            await UnitOfWorkRepo.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
