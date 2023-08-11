using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using TaskMgmt.Model;
using System.Text;

namespace TaskMgmt.WcfService
{
    [ServiceContract]
    public interface ITaskService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "tasks", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Task> GetTasks();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "tasks/{id}", ResponseFormat = WebMessageFormat.Json)]
        Task GetTaskById(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "tasks", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void AddTask(Task task);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "tasks/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateTask(string id, Task task);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "tasks/{id}")]
        void DeleteTask(string id);
    }
}
