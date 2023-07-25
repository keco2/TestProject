using System;
using System.ServiceModel;
using System.Linq;
using TaskMgmt.UI.TaskServiceRef;
using TaskMgmt.UI.MaterialServiceRef;
using TaskMgmt.Model;
using System.Collections.Generic;

namespace TaskMgmt.UI.ViewModel
{
    public class Proxy
    {
        readonly ITaskService _taskService;
        readonly IMaterialService _materialService;

        public Proxy()
        {
            ChannelFactory<ITaskService> taskFactory = new ChannelFactory<ITaskService>("WSHttpBinding_ITaskService");
            ChannelFactory<IMaterialService> materialFactory = new ChannelFactory<IMaterialService>("WSHttpBinding_IMaterialService");

            _taskService = taskFactory.CreateChannel();
            _materialService = materialFactory.CreateChannel();
        }

        public IEnumerable<Material> GetMaterials() => _materialService.GetMaterials();

        public IEnumerable<Task> GetTasks() => _taskService.GetTasks();

        public Task GetTaskById(Guid id) => _taskService.GetTaskById(id.ToString());

        public void AddTask(Task task) => _taskService.AddTask(task);

        public void UpdateTask(Guid id, Task task) => _taskService.UpdateTask(id.ToString(), task);

        public void DeleteTask(Guid id) => _taskService.DeleteTask(id.ToString());
    }
}