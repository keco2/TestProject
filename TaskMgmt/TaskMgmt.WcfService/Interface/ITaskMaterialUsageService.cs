using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TaskMgmt.Model;
using Async = System.Threading.Tasks;

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
        IEnumerable<TaskMaterialUsage> GetTaskMaterialUsagesByTaskId(string taskId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "taskmaterialusage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Async.Task AddTaskMaterialUsageAsync(TaskMaterialUsage taskMaterialUsage);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "taskmaterialusage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Async.Task UpdateTaskMaterialUsageAsync(TaskMaterialUsage taskMaterialUsage);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "taskmaterialusage/{taskId}/{materialId}")]
        Async.Task DeleteTaskMaterialUsageAsync(string taskId, string materialId);
    }
}
