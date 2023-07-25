using System;
using System.ServiceModel;
using System.Linq;
using TaskMgmt.UI.ServiceRef;
using TaskMgmt.Model;
using System.Collections.Generic;

namespace TaskMgmt.UI.ViewModel
{
    public class Proxy
    {
        readonly ITaskService _service;

        public Proxy()
        {
            ChannelFactory<ITaskService> factory = new ChannelFactory<ITaskService>("WsHttpBinding_ITaskService");
            _service = factory.CreateChannel();
        }

        public IEnumerable<Task> GetTasks() => _service.GetTasks();

        public Task GetTaskById(string id) => _service.GetTaskById(id);


        //public List<Task> GetTasks();

        //public Task GetTaskById(string id);

        //public void AddTask(Task task);

        //public void UpdateTask(string id, Task task);

        public void DeleteTask(Guid id) => _service.DeleteTask(id.ToString());
    }
}