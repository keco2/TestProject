using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using TaskMgmt.Model;
using System.Text;
using Async = System.Threading.Tasks;

namespace TaskMgmt.WcfService
{
    [ServiceContract]
    public interface IMaterialService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "materials", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Material> GetMaterials();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "materials/{id}", ResponseFormat = WebMessageFormat.Json)]
        Material GetMaterialById(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "materials", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Async.Task AddMaterialAsync(Material material);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "materials/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Async.Task UpdateMaterialAsync(string id, Material material);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "materials/{id}")]
        Async.Task DeleteMaterialAsync(string id);
    }
}
