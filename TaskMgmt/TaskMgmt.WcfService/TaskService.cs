using System;
using System.Collections.Generic;
using System.Linq;
using TaskMgmt.DAL;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService
{
    public class TaskService : ITaskService
    {
        private IUnitOfWork unitOfWorkRepo;

        public TaskService()
        {
            unitOfWorkRepo = new UnitOfWorkRepository();
        }

        public IEnumerable<Task> GetTasks()
        {
            return unitOfWorkRepo.TaskRepository.GetTasks().ManualMap();
        }

        public Task GetTaskById(string taskId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            return unitOfWorkRepo.TaskRepository.GetTaskByID(taskGuid).ManualMap();
        }

        public void AddTask(Task task)
        {
            unitOfWorkRepo.TaskRepository.InsertTask(task.ManualMap());
            unitOfWorkRepo.SaveChanges();
        }

        public void UpdateTask(string id, Task task)
        {
            Guid taskGuid = Guid.Parse(id);
            unitOfWorkRepo.TaskRepository.UpdateTask(taskGuid, task.ManualMap());
            unitOfWorkRepo.SaveChanges();
        }

        public void DeleteTask(string id)
        {
            Guid taskGuid = Guid.Parse(id);
            unitOfWorkRepo.TaskRepository.DeleteTask(taskGuid);
            unitOfWorkRepo.SaveChanges();
        }
    }

    public static class ManualMapTemp
    {
        public static IEnumerable<Task> ManualMap(this IEnumerable<TaskEntity> dblist)
        {
            return dblist.Select(t => t.ManualMap());
        }

        public static Task ManualMap(this TaskEntity taskEntity)
        {
            return new Task()
            {
                ID = taskEntity.ID,
                Name = taskEntity.Name
            };
        }

        public static TaskEntity ManualMap(this Task task)
        {
            return new TaskEntity()
            {
                ID = task.ID,
                Name = task.Name
            };
        }
    }
}
