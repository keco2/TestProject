using System;
using System.Collections.ObjectModel;
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

    //  <package id = "Unity" version="5.11.10" targetFramework="net48" />
    //  <package id = "Unity.Abstractions" version="5.11.7" targetFramework="net48" />
    //  <package id = "Unity.Container" version="5.11.11" targetFramework="net48" />
    //  <package id = "Unity.Wcf" version="5.11.1" targetFramework="net48" />


    //  https://github.com/unitycontainer/wcf/issues/9

    //  ----------------------------------------------------
    //  Unity.Wcf 5.11.1 not working with Unity 5.11.10  !!!
    //  ----------------------------------------------------


    internal class UnityInstanceProvider : IInstanceProvider
    {

        private readonly IUnityContainer container;
        private readonly Type contractType;

        public UnityInstanceProvider(IUnityContainer container, Type contractType)
        {
            this.container = container;
            this.contractType = contractType;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return container.Resolve(contractType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            //container.Teardown(instance);
        }
    }

    public class UnityServiceBehavior : IServiceBehavior
    {

        private readonly IUnityContainer container;

        public UnityServiceBehavior(IUnityContainer container)
        {
            this.container = container;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    if (endpointDispatcher.ContractName != "IMetadataExchange")
                    {
                        string contractName = endpointDispatcher.ContractName;
                        ServiceEndpoint serviceEndpoint = serviceDescription.Endpoints?.FirstOrDefault(e => e.Contract.Name == contractName);
                        if (serviceEndpoint != null)
                        {
                            endpointDispatcher.DispatchRuntime.InstanceProvider = new UnityInstanceProvider(this.container, serviceEndpoint.Contract.ContractType);
                        }
                    }
                }
            }
        }
    }

    public class UnityServiceHost : ServiceHost
    {

        private IUnityContainer unityContainer;

        public UnityServiceHost(IUnityContainer unityContainer, Type serviceType)
            : base(serviceType)
        {
            this.unityContainer = unityContainer;
        }

        protected override void OnOpening()
        {
            base.OnOpening();

            if (this.Description.Behaviors.Find<UnityServiceBehavior>() == null)
            {
                this.Description.Behaviors.Add(new UnityServiceBehavior(this.unityContainer));
            }
        }
    }









    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Task Management Server";
            Logging.LoggingSetUp();
            ResolveDependencies();
            //StartWcfServices();
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
            //UnityContainer container = new UnityContainer();
            //UnityServiceHost serviceHost = new UnityServiceHost(container, typeof(TaskService));
            //serviceHost.Open();


            IUnityContainer container = new UnityContainer();
            container
                .RegisterType<IUnitOfWork, UnitOfWorkRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<ITaskService, TaskService>(new ContainerControlledLifetimeManager())
                //.RegisterType<TaskService>(new InjectionProperty("UnitOfWorkRepo", new ResolvedArrayParameter<IUnitOfWork>()))
                ;

            UnityServiceHost serviceHost = new UnityServiceHost(container, typeof(TaskService));
            serviceHost.Open();

            ////Resolve the dependency
            //var unitOfWork = container.Resolve<IUnitOfWork>();

            ////Register the instance
            //container = container.RegisterInstance<IUnitOfWork>(unitOfWork);

            ////Resolve for Controller and the dependecy gets injected automatically
            //var controller = container.Resolve<UnitOfWorkRepository>();

            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.ReadLine();
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
