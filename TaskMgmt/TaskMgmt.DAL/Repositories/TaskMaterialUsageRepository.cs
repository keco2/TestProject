using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public class TaskMaterialUsageRepository : IGenericRepository<TaskMaterialUsageEntity>, IDisposable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private bool disposedValue = false;
        private TaskMgmtDbContext context;
        private DbSet<TaskMaterialUsageEntity> dbSet;
        private DbQuery<TaskMaterialUsageEntity> dbQuery;

        public TaskMaterialUsageRepository(TaskMgmtDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TaskMaterialUsageEntity>();
            dbQuery = dbSet.AsNoTracking();
        }

        public void DeleteItem(params Guid[] guids)
        {
            Guid taskGuid = guids[0];
            Guid materialGuid = guids[1];
            logger.Info("{0} {1}={2} {3}={4}", nameof(DeleteItem), nameof(taskGuid), taskGuid, nameof(materialGuid), materialGuid);
            var item = context.TaskMaterialUsages.Where(t => t.Task.ID == taskGuid && t.Material.ID == materialGuid).Single();
            context.TaskMaterialUsages.Remove(item);
        }

        public IEnumerable<TaskMaterialUsageEntity> GetItemsByID(Guid guid)
        {
            logger.Info("{0} {1}={2}", nameof(GetItemsByID), nameof(guid), guid);
            return dbQuery.Where(t => t.Task.ID == guid).Include(m => m.Material).Include(t => t.Task);
        }

        //public IEnumerable<TaskMaterialUsage> GetItemsByTaskID(params Guid[] guids)
        //{
        //    Guid taskGuid = guids[0];
        //    logger.Info("{0} {1}={2}", nameof(GetItemsByTaskID), nameof(taskGuid), taskGuid);
        //    return context.TaskMaterialUsages.Where(t => t.Task.ID == taskGuid);
        //}

        public IEnumerable<TaskMaterialUsageEntity> GetItems()
        {
            logger.Info("{0}", nameof(GetItems));
            return dbQuery.Include(m => m.Material).Include(t => t.Task).AsEnumerable();
        }

        public void InsertItem(TaskMaterialUsageEntity item)
        {
            logger.Info("{0} {1}={2} {3}={4}", nameof(InsertItem), nameof(item.MaterialID), item.MaterialID, nameof(item.TaskID), item.TaskID);
            dbSet.Add(item);
        }

        public void UpdateItem(TaskMaterialUsageEntity item)
        {
            logger.Info("{0} {1}={2} {3}={4}", nameof(UpdateItem), nameof(item.TaskID), item.TaskID, nameof(item.MaterialID), item.MaterialID);
            //TaskMaterialUsageEntity existingItem = context.TaskMaterialUsages.Where(t => t.Task.ID == item.Task.ID && t.Material.ID == item.Material.ID).Single();
            //if (existingItem != null)
            //{
            //    existingItem.Amount = item.Amount;
            //    existingItem.UnitOfMeasurement = item.UnitOfMeasurement;
            //}
            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            logger.Info("Save");
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
