using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TaskMgmt.DAL.Interface;

namespace TaskMgmt.DAL.Repositories
{
    public class TaskRepository : IGenericRepository<TaskEntity>, IDisposable
    {
        private ILogger Logger { get => logger; set => logger = value; }
        private static ILogger logger;
        private bool disposedValue = false;
        private ITaskMgmtDbContext context;
        private IDbSet<TaskEntity> dbSet;
        private IQueryable<TaskEntity> dbQuery;

        public TaskRepository(ITaskMgmtDbContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<TaskEntity>();
            this.dbQuery = dbSet.AsNoTracking();
            Logger = logger;
            logger.Info($"{nameof(TaskRepository)}.Init");
        }

        public void DeleteItem(params Guid[] taskId)
        {
            Logger.Info("TaskRepository.DeleteTask ID={0}", taskId[0]);
            dbSet.Remove(dbSet.Find(taskId[0]));
        }

        public IEnumerable<TaskEntity> GetItemsByID(Guid taskId)
        {
            Logger.Info("TaskRepository.GetTaskByID ID={0}", taskId);
            return dbQuery.Where(t => t.ID == taskId);
        }

        public IEnumerable<TaskEntity> GetItems()
        {
            Logger.Info("TaskRepository.GetTasks");
            return dbQuery.AsEnumerable();
        }

        public void InsertItem(TaskEntity task)
        {
            Logger.Info("TaskRepository.InsertTask ID={0}", task.ID);
            dbSet.Add(task);
        }

        public void UpdateItem(TaskEntity task)
        {
            Logger.Info("TaskRepository.UpdateTask ID={0}", task.ID);
            dbSet.Attach(task);
            context.Entry(task).State = EntityState.Modified;
        }

        public void Save()
        {
            Logger.Info("SaveChanges");
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
