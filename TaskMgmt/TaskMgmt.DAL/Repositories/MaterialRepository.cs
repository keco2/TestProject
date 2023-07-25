using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public class MaterialRepository : IGenericRepository<Material>
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private bool disposedValue = false;
        private DbContext context;

        public MaterialRepository(DbContext context)
        {
            this.context = context;
        }

        void IGenericRepository<Material>.DeleteItem(Guid guid)
        {
            logger.Info("IGenericRepository<Material>.DeleteItem ID={0}", guid);
            context.DbSetMaterials.RemoveAll(t => t.ID == guid);
        }

        Material IGenericRepository<Material>.GetItemByID(Guid guid)
        {
            logger.Info("IGenericRepository<Material>.GetItemByID ID={0}", guid);
            return context.DbSetMaterials.Where(t => t.ID == guid).Single();
        }

        IEnumerable<Material> IGenericRepository<Material>.GetItems()
        {
            logger.Info("IGenericRepository<Material>.GetItems");
            return context.DbSetMaterials.ToList();
        }

        void IGenericRepository<Material>.InsertItem(Material item)
        {
            logger.Info("IGenericRepository<Material>.InsertItem ID={0}", item.ID);
            context.DbSetMaterials.Add(item);
        }

        void IGenericRepository<Material>.Save()
        {
            logger.Info("IGenericRepository<Material>.Save");
            throw new NotImplementedException();
            // Todo - Implement repo.Save - only if real DB provider added
        }

        void IGenericRepository<Material>.UpdateItem(Guid guid, Material item)
        {
            logger.Info("IGenericRepository<Material>.UpdateItem ID={0}", guid);
            Material existingItem = context.DbSetMaterials.Where(b => b.ID == guid).Single();
            if (existingItem != null)
            {
                existingItem.ManufacturerCode = item.ManufacturerCode;
                existingItem.ManufacturerCode = item.ManufacturerCode;
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
