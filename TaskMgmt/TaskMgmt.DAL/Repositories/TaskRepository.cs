using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private bool disposedValue = false;
        private DbContext context;

        public TaskRepository(DbContext context)
        {
            this.context = context;
        }

        void ITaskRepository.DeleteTask(Guid taskId)
        {
            context.Tasks.RemoveAll(t => t.ID == taskId);
        }

        Task ITaskRepository.GetTaskByID(Guid taskId)
        {
            return context.Tasks.Where(t => t.ID == taskId).Single();
        }

        IEnumerable<Task> ITaskRepository.GetTasks()
        {
            return context.Tasks.ToList();
        }

        void ITaskRepository.InsertTask(Task task)
        {
            context.Tasks.Add(task);
        }

        void ITaskRepository.Save()
        {
            throw new NotImplementedException();
            // Todo - Implement repo.Save - only if real DB provider added
        }

        void ITaskRepository.UpdateTask(Guid taskId, Task task)
        {
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
