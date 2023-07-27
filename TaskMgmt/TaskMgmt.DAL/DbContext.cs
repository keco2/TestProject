using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TaskMgmt.Model;

namespace TaskMgmt.DAL
{
    public class DbContext : IDisposable
    {
        public ICollection<Task> Tasks = StaticData.Tasks;
        public ICollection<Material> Materials = StaticData.Materials;
        public ICollection<TaskMaterialUsage> TaskMaterialUsages = StaticData.TaskMaterialUsages;

        public DbContext()
        {
        }

        public void Dispose()
        {
            //
        }
    }
}
