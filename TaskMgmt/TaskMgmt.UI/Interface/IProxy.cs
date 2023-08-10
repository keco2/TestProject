using System;
using System.Collections.Generic;
using TaskMgmt.Model;

namespace TaskMgmt.UI.ViewModel
{
    public interface IProxy
    {
        void AddTask(Task task);
        void AddUsage(TaskMaterialUsage usage);
        void DeleteTask(Guid id);
        void DeleteUsage(Guid taskId, Guid materialId);
        IEnumerable<Material> GetMaterials();
        Task GetTaskById(Guid id);
        IEnumerable<Task> GetTasks();
        IEnumerable<TaskMaterialUsage> GetUsages();
        IEnumerable<TaskMaterialUsage> GetUsagesByTaskId(Guid taskId);
        void UpdateTask(Guid id, Task task);
        void UpdateUsage(TaskMaterialUsage usage);
    }
}