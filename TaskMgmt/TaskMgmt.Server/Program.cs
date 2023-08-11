using NLog;
using System;
using TaskMgmt.Common;
using System.ServiceModel;
using TaskMgmt.WcfService;
using Unity;

namespace TaskMgmt.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Task Management Server";
            Logging.LoggingSetUp();
            StartWcfServices(UnityIoC.ResolveDependencies());
        }

        private static void StartWcfServices(IUnityContainer container)
        {
            using (UnityServiceHost taskServiceHost = new UnityServiceHost(container, typeof(TaskService)))
            using (UnityServiceHost materialServiceHost = new UnityServiceHost(container, typeof(MaterialService)))
            using (UnityServiceHost taskMaterialUsageServiceHost = new UnityServiceHost(container, typeof(TaskMaterialUsageService)))
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

        private static void IncreaseServiehostDebugTimeout(ServiceHost serviceHost)
        {
#if DEBUG
            serviceHost.OpenTimeout = new TimeSpan(0, 15, 0);
            serviceHost.CloseTimeout = new TimeSpan(0, 15, 0);
#endif
        }
    }
}
