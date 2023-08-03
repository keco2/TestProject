using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;
using TaskMgmt.Common;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Repositories;
using TaskMgmt.WcfService;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Wcf;

namespace TaskMgmt.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Task Management Server";
            Logging.LoggingSetUp();
            ResolveDependencies();
            StartWcfServices();
        }

        private static void StartWcfServices()
        {
            using (ServiceHost taskServiceHost = new ServiceHost(typeof(TaskService)))
            using (ServiceHost materialServiceHost = new ServiceHost(typeof(MaterialService)))
            using (ServiceHost taskMaterialUsageServiceHost = new ServiceHost(typeof(TaskMaterialUsageService)))
            {
                try
                {
                    IncreaseServiehostDebugTimeout(taskServiceHost);
                    IncreaseServiehostDebugTimeout(materialServiceHost);
                    IncreaseServiehostDebugTimeout(taskMaterialUsageServiceHost);

                    taskServiceHost.Open();
                    materialServiceHost.Open();
                    taskMaterialUsageServiceHost.Open();

                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.ReadLine();

                    taskServiceHost.Close();
                    materialServiceHost.Close();
                    taskMaterialUsageServiceHost.Close();
                }
                catch (TimeoutException timeProblem)
                {
                    Console.WriteLine(timeProblem.Message);
                    Console.ReadLine();
                }
                catch (CommunicationException commProblem)
                {
                    Console.WriteLine(commProblem.Message);
                    Console.ReadLine();
                }
            }
        }

        private static void ResolveDependencies()
        {
            IUnityContainer container = new UnityContainer();
            container
                .RegisterType<IUnitOfWork, UnitOfWorkRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<ITaskService, TaskService>(new ContainerControlledLifetimeManager())
                .RegisterType<TaskService>(new InjectionProperty("UnitOfWorkRepo", new ResolvedArrayParameter<IUnitOfWork>()));

            //Resolve the dependency
            //var unitOfWork = container.Resolve<IUnitOfWork>();

            //Register the instance
            //container = container.RegisterInstance<IUnitOfWork>(unitOfWork);

            //Resolve for Controller and the dependecy gets injected automatically
            //var controller = container.Resolve<UnitOfWorkRepository>();
        }

        private static void IncreaseServiehostDebugTimeout(ServiceHost serviceHost)
        {
#if DEBUG
            serviceHost.OpenTimeout = new TimeSpan(0, 15, 0);
            serviceHost.CloseTimeout = new TimeSpan(0, 15, 0);
#endif
        }
    }
}
