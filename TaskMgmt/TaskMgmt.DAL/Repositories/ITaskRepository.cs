using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public interface ITaskRepository : IDisposable
    {
        IEnumerable<TaskEntity> GetTasks();
        TaskEntity GetTaskByID(Guid taskId);
        void InsertTask(TaskEntity task);
        void UpdateTask(Guid taskId, TaskEntity task);
        void DeleteTask(Guid taskId);
        void Save();
    }
}
