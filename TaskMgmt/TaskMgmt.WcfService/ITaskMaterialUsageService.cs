using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService
{
    [ServiceContract]
    public interface ITaskMaterialUsageService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "taskmaterialusage", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<TaskMaterialUsage> GetTaskMaterialUsages();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "taskmaterialusage/{taskId}", ResponseFormat = WebMessageFormat.Json)]
        TaskMaterialUsage GetTaskMaterialUsageByTaskId(string taskId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "taskmaterialusage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void AddTaskMaterialUsage(TaskMaterialUsage taskMaterialUsage);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "taskmaterialusage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateTaskMaterialUsage(TaskMaterialUsage taskMaterialUsage);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "taskmaterialusage/{taskId}/{materialId}")]
        void DeleteTaskMaterialUsage(string taskId, string materialId);
    }
}
