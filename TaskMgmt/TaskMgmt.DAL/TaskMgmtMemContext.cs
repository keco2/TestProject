using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TaskMgmt.Model;

namespace TaskMgmt.DAL
{
    public class TaskMgmtMemContext : IDisposable
    {
        public ICollection<Task> Tasks = StaticData.Tasks;
        public ICollection<Material> Materials = StaticData.Materials;
        public ICollection<TaskMaterialUsage> TaskMaterialUsages = StaticData.TaskMaterialUsages;

        public TaskMgmtMemContext()
        {
        }

        public void Dispose()
        {
            //
        }
    }
}
