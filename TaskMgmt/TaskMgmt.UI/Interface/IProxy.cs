using System;
using System.Collections.Generic;
using TaskMgmt.Model;
using Async = System.Threading.Tasks;

namespace TaskMgmt.UI.ViewModel
{
    public interface IProxy
    {
        Async.Task AddMaterialAsync(Material material);
        Async.Task AddTaskAsync(Task task);
        Async.Task AddUsageAsync(TaskMaterialUsage usage);
        Async.Task DeleteMaterialAsync(Guid id);
        Async.Task DeleteTaskAsync(Guid id);
        Async.Task DeleteUsageAsync(Guid taskId, Guid materialId);
        Material GetMaterialById(Guid id);
        IEnumerable<Material> GetMaterials();
        Task GetTaskById(Guid id);
        IEnumerable<Task> GetTasks();
        IEnumerable<TaskMaterialUsage> GetUsages();
        IEnumerable<TaskMaterialUsage> GetUsagesByTaskId(Guid taskId);
        Async.Task UpdateMaterialAsync(Guid id, Material material);
        Async.Task UpdateTaskAsync(Guid id, Task task);
        Async.Task UpdateUsageAsync(TaskMaterialUsage usage);
    }
}