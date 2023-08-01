using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public class MaterialRepository : IGenericRepository<MaterialEntity>, IDisposable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private bool disposedValue = false;
        private TaskMgmtDbContext context;
        private DbSet<MaterialEntity> dbSet;
        private DbQuery<MaterialEntity> dbQuery;

        public MaterialRepository(TaskMgmtDbContext context)
        {
            this.context = context;
            dbSet = context.Set<MaterialEntity>();
            dbQuery = dbSet.AsNoTracking();
        }

        public void DeleteItem(params Guid[] guids)
        {
            logger.Info("DeleteItem ID={0}", guids);

            var item = dbSet.Find(guids[0]);
            dbSet.Remove(item);
        }

        public IEnumerable<MaterialEntity> GetItemsByID(Guid guid)
        {
            logger.Info("GetItemByID ID={0}", guid);
            return dbQuery.Where(t => t.ID == guid);
        }

        public IEnumerable<MaterialEntity> GetItems()
        {
            logger.Info("GetItems");
            return dbQuery.AsEnumerable(); ;
        }

        public void InsertItem(MaterialEntity item)
        {
            logger.Info("InsertItem ID={0}", item.ID);
            dbSet.Add(item);
        }

        public void UpdateItem(MaterialEntity item)
        {
            logger.Info("UpdateItem ID={0}", item.ID);
            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
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
