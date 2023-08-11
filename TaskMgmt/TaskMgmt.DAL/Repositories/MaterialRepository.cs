using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TaskMgmt.DAL.Interface;

namespace TaskMgmt.DAL.Repositories
{
    public class MaterialRepository : IGenericRepository<MaterialEntity>, IDisposable
    {
        private ILogger Logger { get => logger; set => logger = value; }
        private static ILogger logger;
        private bool disposedValue = false;
        private ITaskMgmtDbContext context;
        private IDbSet<MaterialEntity> dbSet;
        private IQueryable<MaterialEntity> dbQuery;

        public MaterialRepository(ITaskMgmtDbContext context, ILogger logger)
        {
            this.context = context;
            dbSet = context.Set<MaterialEntity>();
            dbQuery = dbSet.AsNoTracking();
            Logger = logger;
            logger.Info($"{nameof(MaterialRepository)}.Init");
        }

        public void DeleteItem(params Guid[] guids)
        {
            Logger.Info("DeleteItem ID={0}", guids);

            var item = dbSet.Find(guids[0]);
            dbSet.Remove(item);
        }

        public IEnumerable<MaterialEntity> GetItemsByID(Guid guid)
        {
            Logger.Info("GetItemByID ID={0}", guid);
            return dbQuery.Where(t => t.ID == guid);
        }

        public IEnumerable<MaterialEntity> GetItems()
        {
            Logger.Info("GetItems");
            return dbQuery.AsEnumerable(); ;
        }

        public void InsertItem(MaterialEntity item)
        {
            Logger.Info("InsertItem ID={0}", item.ID);
            dbSet.Add(item);
        }

        public void UpdateItem(MaterialEntity item)
        {
            Logger.Info("UpdateItem ID={0}", item.ID);
            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
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
