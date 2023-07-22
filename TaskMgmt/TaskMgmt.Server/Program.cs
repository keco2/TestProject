using System;
using System.ServiceModel;
using TaskMgmt.WcfService;

namespace TaskMgmt.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";

            using (ServiceHost serviceHost = new ServiceHost(typeof(TaskService)))
            {
                try
                {
                    serviceHost.Open();

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
