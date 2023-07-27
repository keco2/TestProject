using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private bool disposedValue = false;
        private DbContext context;

        public TaskRepository(DbContext context)
        {
            this.context = context;
        }

        void ITaskRepository.DeleteTask(Guid taskId)
        {
            logger.Info("TaskRepository.DeleteTask ID={0}", taskId);
            context.Tasks.Remove(context.Tasks.Where(t => t.ID == taskId).Single());
        }

        Task ITaskRepository.GetTaskByID(Guid taskId)
        {
            logger.Info("TaskRepository.GetTaskByID ID={0}", taskId);
            return context.Tasks.Where(t => t.ID == taskId).Single();
        }

        IEnumerable<Task> ITaskRepository.GetTasks()
        {
            logger.Info("TaskRepository.GetTasks");
            return context.Tasks;
        }

        void ITaskRepository.InsertTask(Task task)
        {
            logger.Info("TaskRepository.InsertTask ID={0}", task.ID);
            context.Tasks.Add(task);
        }

        void ITaskRepository.Save()
        {
            logger.Info("TaskRepository.Save");
            throw new NotImplementedException();
            // Todo - Implement repo.Save - only if real DB provider added
        }

        void ITaskRepository.UpdateTask(Guid taskId, Task task)
        {
            logger.Info("TaskRepository.UpdateTask ID={0}", taskId);
            Task existingTask = context.Tasks.Where(b => b.ID == taskId).Single();
            if (existingTask != null)
            {
                existingTask.Name = task.Name;
                // Todo - Include all fields - ITaskRepository.UpdateTask
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
