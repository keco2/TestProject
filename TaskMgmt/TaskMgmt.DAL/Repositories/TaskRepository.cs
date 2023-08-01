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
    public class TaskRepository : IGenericRepository<TaskEntity>, IDisposable
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

        public void DeleteItem(params Guid[] taskId)
        {
            logger.Info("TaskRepository.DeleteTask ID={0}", taskId[0]);
            dbSet.Remove(dbSet.Find(taskId[0]));
        }

        public IEnumerable<TaskEntity> GetItemsByID(Guid taskId)
        {
            logger.Info("TaskRepository.GetTaskByID ID={0}", taskId);
            return dbQuery.Where(t => t.ID == taskId);
        }

        public IEnumerable<TaskEntity> GetItems()
        {
            logger.Info("TaskRepository.GetTasks");
            return dbQuery.AsEnumerable();
        }

        public void InsertItem(TaskEntity task)
        {
            logger.Info("TaskRepository.InsertTask ID={0}", task.ID);
            dbSet.Add(task);
        }

        public void UpdateItem(TaskEntity task)
        {
            logger.Info("TaskRepository.UpdateTask ID={0}", task.ID);
            dbSet.Attach(task);
            context.Entry(task).State = EntityState.Modified;
        }

        public void Save()
        {
            logger.Info("SaveChanges");
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
