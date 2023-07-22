using System;
using System.ServiceModel;
using System.Linq;
using TaskMgmt.UI.ServiceRef;

namespace TaskMgmt.UI.ViewModel
{
    internal sealed class Proxy
    {
        readonly ITaskService _service;

        internal Proxy()
        {
            ChannelFactory<ITaskService> factory = new ChannelFactory<ITaskService>("BasicHttpBinding_ITaskService");
            _service = factory.CreateChannel();
        }

        internal string GetTasks()
        {
            return _service.GetTasks().First().Name;
        }
    }
}