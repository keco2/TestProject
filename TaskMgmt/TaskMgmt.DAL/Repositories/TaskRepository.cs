using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private bool disposedValue = false;
        private TaskMgmtDbContext context;
        private DbSet<TaskEntity> dbSet;
        private DbQuery<TaskEntity> dbQuery;

        public TaskRepository(TaskMgmtDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TaskEntity>();
            this.dbQuery = dbSet.AsNoTracking();
        }

        public void DeleteTask(Guid taskId)
        {
            logger.Info("TaskRepository.DeleteTask ID={0}", taskId);
            dbSet.Remove(dbSet.Find(taskId));
        }

        public TaskEntity GetTaskByID(Guid taskId)
        {
            logger.Info("TaskRepository.GetTaskByID ID={0}", taskId);
            return dbQuery.Where(t => t.ID == taskId).Single();
        }

        public IEnumerable<TaskEntity> GetTasks()
        {
            logger.Info("TaskRepository.GetTasks");
            return dbQuery.AsEnumerable();
        }

        public void InsertTask(TaskEntity task)
        {
            logger.Info("TaskRepository.InsertTask ID={0}", task.ID);
            dbSet.Add(task);
        }

        public void UpdateTask(Guid taskId, TaskEntity task)
        {
            logger.Info("TaskRepository.UpdateTask ID={0}", taskId);
            dbSet.Attach(task);
            context.Entry(task).State = EntityState.Modified;
        }

        public void Save()
        {
            logger.Info("TaskRepository.Save");
            context.SaveChanges();
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
