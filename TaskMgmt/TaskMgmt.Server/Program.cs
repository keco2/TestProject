using System;
using System.ServiceModel;
using TaskMgmt.Common;
using TaskMgmt.WcfService;

namespace TaskMgmt.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Logging.LoggingSetUp();
            Console.Title = "Task Management Server";

            using (ServiceHost taskServiceHost = new ServiceHost(typeof(TaskService)))
            using (ServiceHost materialServiceHost = new ServiceHost(typeof(MaterialService)))
            using (ServiceHost taskMaterialUsageServiceHost = new ServiceHost(typeof(TaskMaterialUsageService)))
            {
                try
                {
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
    }
}
