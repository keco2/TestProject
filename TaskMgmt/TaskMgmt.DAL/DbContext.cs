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
        public List<Task> Tasks = StaticData.Tasks;

        public DbContext()
        {
        }

        public void Dispose()
        {
            //
        }
    }
}
