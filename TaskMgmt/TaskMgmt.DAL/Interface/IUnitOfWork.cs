namespace TaskMgmt.DAL.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<TaskMaterialUsageEntity> TaskMaterialUsageRepository { get; }

        IGenericRepository<MaterialEntity> MaterialRepository { get; }

        IGenericRepository<TaskEntity> TaskRepository { get; }

        System.Threading.Tasks.Task SaveChangesAsync();
    }
}