using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DAL.Repositories;
using TaskMgmt.Model;

namespace TaskMgmt.DAL
{
    public class UnitOfWorkRepository : IUnitOfWork, IDisposable
    {
        private IGenericRepository<TaskEntity> taskRepository;
        private IGenericRepository<MaterialEntity> materialRepository;
        private IGenericRepository<TaskMaterialUsageEntity> taskMaterialUsageRepository;
        private TaskMgmtDbContext dbContext = new TaskMgmtDbContext();
        private bool disposedValue;

        public IGenericRepository<TaskEntity> TaskRepository
        {
            get
            {
                if (taskRepository == null)
                {
                    taskRepository = new TaskRepository(dbContext);
                }
                return taskRepository;
            }
        }

        public IGenericRepository<MaterialEntity> MaterialRepository
        {
            get
            {
                if (materialRepository == null)
                {
                    materialRepository = new MaterialRepository(dbContext);
                }
                return materialRepository;
            }
        }

        public IGenericRepository<TaskMaterialUsageEntity> TaskMaterialUsageRepository
        {
            get
            {
                if (taskMaterialUsageRepository == null)
                {
                    taskMaterialUsageRepository = new TaskMaterialUsageRepository(dbContext);
                }
                return taskMaterialUsageRepository;
            }
        }


        public void SaveChanges()
        {
            dbContext.SaveChanges();
            DetachAll();
        }

        private void DetachAll()
        {
            foreach (DbEntityEntry e in dbContext.ChangeTracker.Entries().ToList())
            {
                e.State = System.Data.Entity.EntityState.Detached;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
