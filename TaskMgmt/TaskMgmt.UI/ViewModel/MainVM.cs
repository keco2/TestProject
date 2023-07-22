using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.UI.ServiceRef;

namespace TaskMgmt.UI.ViewModel
{
    class MainVM
    {
        private string taskName = "TEST";

        public MainVM()
        {
            HookUpUICommands();
            GetWcf();
        }

        private void GetWcf()
        {
            //string baseAddress = "http://localhost:8000/TaskMgmt.WcfService/TaskService/mex";     BAD REQUEST
            //string baseAddress = "http://localhost:8000/TaskMgmt.WcfService/TaskService/";        NOT ALLOWED
            //string baseAddress = "http://localhost:8000/TaskMgmt.WcfService_xxX/TaskService/";    NOT FOUND
            string baseAddress = "http://localhost:8000/TaskMgmt.WcfService/TaskService";

            //TaskName = GetData_v1(baseAddress);
            TaskName = GetData_v2(baseAddress);

        }

        private string GetData_v1(string baseAddress)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //return httpClient.GetAsync("tasks").Result.ToString();
            return httpClient.GetStringAsync("tasks").Result.ToString();

            //HttpResponseMessage response = httpClient.GetAsync("tasks").Result;
            //
            //if (response.IsSuccessStatusCode)
            //{
            //    //List<TaskMgmt.Model.Task> tasks = response.Content.ReadAsStringAsync<TaskMgmt.Model.Task>().Result; // ReadAsAsync
            //    //TaskName = tasks.First().Name;
            //}
        }

        private string GetData_v2(string baseAddress)
        {
            ChannelFactory<ITaskService> factory = new ChannelFactory<ITaskService>(new BasicHttpBinding(), new EndpointAddress(baseAddress));
            ITaskService service = factory.CreateChannel();

            return service.GetTasks().First().Name;
        }

        private void HookUpUICommands()
        {
            //throw new NotImplementedException();
        }

        public string TaskName { get => taskName; set => taskName = value; }
    }
}
