using NLog;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TaskMgmt.DAL.Interface;
using TaskMgmt.DAL.Repositories;
using Unity;

namespace TaskMgmt.DAL
{
    public class UnitOfWorkRepository : IUnitOfWork, IDisposable
    {
        private ILogger logger { get => _logger; set => _logger = value; }
        private static ILogger _logger;
        private ITaskMgmtDbContext dbContext;
        private bool disposedValue;

        public UnitOfWorkRepository(ITaskMgmtDbContext taskMgmtDbContext, ILogger logger)
        {
            dbContext = taskMgmtDbContext;
            this.logger = logger;
        }

        [Dependency]
        public Lazy<IGenericRepository<TaskEntity>> TaskRepositoryLazy { get; set; }

        [Dependency]
        public Lazy<IGenericRepository<MaterialEntity>> MaterialRepositoryLazy { get; set; }

        [Dependency]
        public Lazy<IGenericRepository<TaskMaterialUsageEntity>> TaskMaterialUsageRepositoryLazy { get; set; }

        public IGenericRepository<TaskEntity> TaskRepository
        {
            get => TaskRepositoryLazy.Value;
        }

        public IGenericRepository<MaterialEntity> MaterialRepository
        {
            get => MaterialRepositoryLazy.Value;
        }

        public IGenericRepository<TaskMaterialUsageEntity> TaskMaterialUsageRepository
        {
            get => TaskMaterialUsageRepositoryLazy.Value;
        }

        public async System.Threading.Tasks.Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
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
