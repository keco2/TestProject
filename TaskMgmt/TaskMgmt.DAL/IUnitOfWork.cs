using TaskMgmt.DAL.Repositories;
using TaskMgmt.Model;

namespace TaskMgmt.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<TaskMaterialUsageEntity> TaskMaterialUsageRepository { get; }

        IGenericRepository<MaterialEntity> MaterialRepository { get; }

        IGenericRepository<TaskEntity> TaskRepository { get; }

        void SaveChanges();
    }
}