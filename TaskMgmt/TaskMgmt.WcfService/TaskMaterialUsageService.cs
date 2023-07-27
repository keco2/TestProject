using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Repositories;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService
{
    public class TaskMaterialUsageService : ITaskMaterialUsageService
    {
        private TaskMaterialUsageRepository repo;

        public TaskMaterialUsageService()
        {
            this.repo = new TaskMaterialUsageRepository(new DbContext());
        }

        public IEnumerable<TaskMaterialUsage> GetTaskMaterialUsages()
        {
            return repo.GetItems();
        }

        public IEnumerable<TaskMaterialUsage> GetTaskMaterialUsagesByTaskId(string taskId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            return repo.GetItemsByTaskID(taskGuid);
        }

        public void AddTaskMaterialUsage(TaskMaterialUsage usage)
        {
            repo.InsertItem(usage);
        }

        public void UpdateTaskMaterialUsage(TaskMaterialUsage usage)
        {
            repo.UpdateItem(usage);
        }

        public void DeleteTaskMaterialUsage(string taskId, string materialId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            Guid materialGuid = Guid.Parse(materialId);
            repo.DeleteItem(taskGuid, materialGuid);
        }
    }
}
