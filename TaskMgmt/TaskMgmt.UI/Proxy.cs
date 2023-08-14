using System;
using System.ServiceModel;
using TaskMgmt.UI.TaskServiceRef;
using TaskMgmt.UI.MaterialServiceRef;
using TaskMgmt.UI.TaskMaterialUsageServiceRef;
using TaskMgmt.Model;
using System.Collections.Generic;
using Async = System.Threading.Tasks;

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

        public Material GetMaterialById(Guid id) => _materialService.GetMaterialById(id.ToString());

        public async Async.Task AddMaterialAsync(Material material) => await _materialService.AddMaterialAsync(material);

        public async Async.Task UpdateMaterialAsync(Guid id, Material material) => await _materialService.UpdateMaterialAsync(id.ToString(), material);

        public async Async.Task DeleteMaterialAsync(Guid id) => await _materialService.DeleteMaterialAsync(id.ToString());


        public IEnumerable<TaskMaterialUsage> GetUsages() => _taskMaterialUsageService.GetTaskMaterialUsages();

        public IEnumerable<TaskMaterialUsage> GetUsagesByTaskId(Guid taskId) => _taskMaterialUsageService.GetTaskMaterialUsagesByTaskId(taskId.ToString());

        public async Async.Task AddUsageAsync(TaskMaterialUsage usage) => await _taskMaterialUsageService.AddTaskMaterialUsageAsync(usage);

        public async Async.Task UpdateUsageAsync(TaskMaterialUsage usage) => await _taskMaterialUsageService.UpdateTaskMaterialUsageAsync(usage);

        public async Async.Task DeleteUsageAsync(Guid taskId, Guid materialId) => await _taskMaterialUsageService.DeleteTaskMaterialUsageAsync(taskId.ToString(), materialId.ToString());


        public IEnumerable<Task> GetTasks() => _taskService.GetTasks();

        public Task GetTaskById(Guid id) => _taskService.GetTaskById(id.ToString());

        public async Async.Task AddTaskAsync(Task task) => await _taskService.AddTaskAsync(task);

        public async Async.Task UpdateTaskAsync(Guid id, Task task) => await _taskService.UpdateTaskAsync(id.ToString(), task);

        public async Async.Task DeleteTaskAsync(Guid id) => await _taskService.DeleteTaskAsync(id.ToString());
    }
}