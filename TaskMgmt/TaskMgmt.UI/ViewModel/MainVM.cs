using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
            //string baseAddress = "http://localhost:8733/TaskService.svc"; // 8080 Replace with the actual address of your service.
            //string baseAddress = "http://localhost:8733/Design_Time_Addresses/TaskMgmt.WcfService/TaskService/";
            string baseAddress = "http://localhost:8733/TaskMgmt.WcfService/TaskService/";

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = httpClient.GetAsync("tasks").Result;
            //
            //if (response.IsSuccessStatusCode)
            //{
            //    //List<TaskMgmt.Model.Task> tasks = response.Content.ReadAsStringAsync<TaskMgmt.Model.Task>().Result; // ReadAsAsync
            //    //TaskName = tasks.First().Name;
            //}

        }

        private void HookUpUICommands()
        {
            //throw new NotImplementedException();
        }

        public string TaskName { get => taskName; set => taskName = value; }
    }
}
