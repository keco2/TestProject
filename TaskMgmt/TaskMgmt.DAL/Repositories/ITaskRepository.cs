using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.DAL.Repositories
{
    public interface ITaskRepository : IDisposable
    {
        IEnumerable<Task> GetTasks();
        Task GetTaskByID(Guid taskId);
        void InsertTask(Task task);
        void UpdateTask(Guid taskId, Task task);
        void DeleteTask(Guid taskId);
        void Save();
    }
}
