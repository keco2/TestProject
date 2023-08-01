using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public class MaterialRepository : IGenericRepository<Material>, IDisposable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private bool disposedValue = false;
        private TaskMgmtMemContext context;

        public MaterialRepository(TaskMgmtMemContext context)
        {
            this.context = context;
        }

        public void DeleteItem(params Guid[] guids)
        {
            logger.Info("DeleteItem ID={0}", guids);

            var item = context.Materials.Where(t => t.ID == guids[0]).Single();
            context.Materials.Remove(item);
        }

        public IEnumerable<Material> GetItemsByID(Guid guid)
        {
            logger.Info("GetItemByID ID={0}", guid);
            return context.Materials.Where(t => t.ID == guid);
        }

        public IEnumerable<Material> GetItems()
        {
            logger.Info("GetItems");
            return context.Materials;
        }

        public void InsertItem(Material item)
        {
            logger.Info("InsertItem ID={0}", item.ID);
            context.Materials.Add(item);
        }

        public void Save()
        {
            logger.Info("Save");
            throw new NotImplementedException();
            // Todo - Implement repo.Save - only if real DB provider added
        }

        public void UpdateItem(Material item)
        {
            logger.Info("UpdateItem ID={0}", item.ID);
            Material existingItem = context.Materials.Where(i => i.ID == item.ID).Single();
            if (existingItem != null)
            {
                existingItem.Partnumber = item.Partnumber;
                existingItem.ManufacturerCode = item.ManufacturerCode;
                existingItem.Price = item.Price;
                existingItem.UnitOfIssue = item.UnitOfIssue;
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
