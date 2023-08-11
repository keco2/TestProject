using TaskMgmt.DAL;
using TaskMgmt.DAL.Interface;
using TaskMgmt.DAL.Repositories;
using TaskMgmt.WcfService;
using Unity;
using Unity.Lifetime;
using NLog;

namespace TaskMgmt.Server
{
    public static class UnityIoC
    {
        public static IUnityContainer ResolveDependencies()
        {
            IUnityContainer container = new UnityContainer();
            container
                .RegisterFactory<ILogger>(l => LogManager.GetCurrentClassLogger())
                .RegisterType<IGenericRepository<TaskMaterialUsageEntity>, TaskMaterialUsageRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IGenericRepository<MaterialEntity>, MaterialRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IGenericRepository<TaskEntity>, TaskRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<ITaskMgmtDbContext, TaskMgmtDbContext>(new ContainerControlledLifetimeManager())
                .RegisterType<ITaskMgmtDbContext, TaskMgmtDbContext>(new ContainerControlledLifetimeManager())
                .RegisterType<IUnitOfWork, UnitOfWorkRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<ITaskService, TaskService>(new ContainerControlledLifetimeManager())
                .RegisterType<IMaterialService, MaterialService>(new ContainerControlledLifetimeManager())
                .RegisterType<ITaskMaterialUsageService, TaskMaterialUsageService>(new ContainerControlledLifetimeManager())
                //.RegisterType<TaskService>(new InjectionProperty("UnitOfWorkRepo", new ResolvedArrayParameter<IUnitOfWork>()))
                ;
            return container;
        }
    }
}