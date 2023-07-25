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

            using (ServiceHost serviceHost = new ServiceHost(typeof(TaskService)))
            using (ServiceHost serviceHost2 = new ServiceHost(typeof(MaterialService)))
            {
                try
                {
                    serviceHost.Open();
                    serviceHost2.Open();

                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.ReadLine();

                    serviceHost.Close();
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
