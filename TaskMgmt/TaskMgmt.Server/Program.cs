using System;
using System.Linq;
using System.ServiceModel;
using TaskMgmt.Common;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Repositories;
using TaskMgmt.WcfService;

namespace TaskMgmt.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Logging.LoggingSetUp();
            Console.Title = "Task Management Server";

            //ITaskRepository repo;
            //var dbcontext = new TaskMgmtDbContext();
            ////Console.WriteLine(dbcontext.Database.Connection.ServerVersion);
            //repo = new TaskRepository(dbcontext);
            //var x = repo.GetTasks();
            //Console.WriteLine(x.First().Name);

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

        private static void IncreaseServiehostDebugTimeout(ServiceHost serviceHost)
        {
#if DEBUG
            serviceHost.OpenTimeout = new TimeSpan(0, 15, 0);
            serviceHost.CloseTimeout = new TimeSpan(0, 15, 0);
#endif
        }
    }
}
