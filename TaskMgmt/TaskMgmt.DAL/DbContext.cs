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
        public List<Task> Tasks;

        public DbContext()
        {
            TestSeed();
        }

        private void TestSeed()
        {
            Tasks = new List<Task>
            {
                new Task { ID = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Task 1" },
                new Task { ID = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Task 2" },
                new Task { ID = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Task 3" }
            };
        }

        public void Dispose()
        {
            //
        }
    }
}
