using TaskMgmt.DAL.Repositories;
using TaskMgmt.Model;

namespace TaskMgmt.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<TaskMaterialUsage> TaskMaterialUsageRepository { get; }

        IGenericRepository<Material> MaterialRepository { get; }

        ITaskRepository TaskRepository { get; }

        void SaveChanges();
    }
}