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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITaskService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "tasks", ResponseFormat = WebMessageFormat.Json)]
        List<Task> GetTasks();

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

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfService.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
