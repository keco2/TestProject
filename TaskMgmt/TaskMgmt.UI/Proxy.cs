using System;
using System.ServiceModel;
using TaskMgmt.UI.TaskServiceRef;
using TaskMgmt.UI.MaterialServiceRef;
using TaskMgmt.UI.TaskMaterialUsageServiceRef;
using TaskMgmt.Model;
using System.Collections.Generic;

namespace TaskMgmt.UI.ViewModel
{
    public class Proxy : IProxy
    {
        readonly ITaskService _taskService;
        readonly IMaterialService _materialService;
        readonly ITaskMaterialUsageService _taskMaterialUsageService;

        public Proxy()
        {
            ChannelFactory<ITaskService> taskFactory = new ChannelFactory<ITaskService>("WSHttpBinding_ITaskService");
            ChannelFactory<IMaterialService> materialFactory = new ChannelFactory<IMaterialService>("WSHttpBinding_IMaterialService");
            ChannelFactory<ITaskMaterialUsageService> taskMaterialUsageFactory = new ChannelFactory<ITaskMaterialUsageService>("WSHttpBinding_ITaskMaterialUsageService");

            _taskService = taskFactory.CreateChannel();
            _materialService = materialFactory.CreateChannel();
            _taskMaterialUsageService = taskMaterialUsageFactory.CreateChannel();
        }

        public IEnumerable<Material> GetMaterials() => _materialService.GetMaterials();
        // ...
        // ...

        public IEnumerable<TaskMaterialUsage> GetUsages() => _taskMaterialUsageService.GetTaskMaterialUsages();

        public IEnumerable<TaskMaterialUsage> GetUsagesByTaskId(Guid taskId) => _taskMaterialUsageService.GetTaskMaterialUsagesByTaskId(taskId.ToString());

        public void AddUsage(TaskMaterialUsage usage) => _taskMaterialUsageService.AddTaskMaterialUsage(usage);

        public void UpdateUsage(TaskMaterialUsage usage) => _taskMaterialUsageService.UpdateTaskMaterialUsage(usage);

        public void DeleteUsage(Guid taskId, Guid materialId) => _taskMaterialUsageService.DeleteTaskMaterialUsage(taskId.ToString(), materialId.ToString());


        public IEnumerable<Task> GetTasks() => _taskService.GetTasks();

        public Task GetTaskById(Guid id) => _taskService.GetTaskById(id.ToString());

        public void AddTask(Task task) => _taskService.AddTask(task);

        public void UpdateTask(Guid id, Task task) => _taskService.UpdateTask(id.ToString(), task);

        public void DeleteTask(Guid id) => _taskService.DeleteTask(id.ToString());
    }
}