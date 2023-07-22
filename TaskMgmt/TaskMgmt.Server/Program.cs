using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TaskMgmt.WcfService;
using System.ServiceModel.Description;

namespace TaskMgmt.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";

            //string baseAddress = "http://localhost:8000/TaskMgmt.WcfService/TaskService/";
            string baseAddress = "http://localhost:8000/TaskMgmt.WcfService/";
            //RunHost_MsHowToHostAndRunABasicWcfService(baseAddress);
            RunHost_githubVanHakobyanWCFprojectsHostingServiceWCF(baseAddress);
        }

        private static void RunHost_githubVanHakobyanWCFprojectsHostingServiceWCF(string baseAddress)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(TaskService), new Uri("http://localhost:8000/TaskMgmt.WcfService/TaskService")))
            {
                serviceHost.AddServiceEndpoint(typeof(ITaskService), new BasicHttpBinding(), "");

                try
                {
                    // Open the ServiceHost to start listening for messages.
                    serviceHost.Open();

                    // The service can now be accessed.
                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.ReadLine();

                    // Close the ServiceHost.
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

        private static void RunHost_MsHowToHostAndRunABasicWcfService(string baseAddress)
        {
            ServiceHost selfHost = new ServiceHost(typeof(TaskService), new Uri(baseAddress));

            try
            {
                // Step 3: Add a service endpoint.
                selfHost.AddServiceEndpoint(typeof(ITaskService), new WSHttpBinding(), "TaskService");

                // Step 4: Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5: Start the service.
                selfHost.Open();
                Console.WriteLine("The service is ready.");

                // Close the ServiceHost to stop the service.
                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}
