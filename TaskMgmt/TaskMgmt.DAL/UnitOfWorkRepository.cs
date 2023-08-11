using NLog;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TaskMgmt.DAL.Interface;
using TaskMgmt.DAL.Repositories;

namespace TaskMgmt.DAL
{
    public class UnitOfWorkRepository : IUnitOfWork, IDisposable
    {
        private ILogger logger { get => _logger; set => _logger = value; }
        private static ILogger _logger;
        private IGenericRepository<TaskEntity> taskRepository;
        private IGenericRepository<MaterialEntity> materialRepository;
        private IGenericRepository<TaskMaterialUsageEntity> taskMaterialUsageRepository;
        private ITaskMgmtDbContext dbContext;
        private bool disposedValue;

        public UnitOfWorkRepository(ITaskMgmtDbContext taskMgmtDbContext, ILogger logger)
        {
            dbContext = taskMgmtDbContext;
            this.logger = logger;
        }

        public IGenericRepository<TaskEntity> TaskRepository
        {
            get
            {
                if (taskRepository == null)
                {
                    taskRepository = new TaskRepository(dbContext, logger);
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
                    materialRepository = new MaterialRepository(dbContext, logger);
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
                    taskMaterialUsageRepository = new TaskMaterialUsageRepository(dbContext, logger);
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
