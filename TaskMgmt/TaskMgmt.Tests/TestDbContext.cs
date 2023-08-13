using System.Data.Common;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Interface;

namespace TaskMgmt.Tests
{
    public class TestDbContext : TaskMgmtDbContext, ITaskMgmtDbContext
    {
        public TestDbContext() : base(Effort.DbConnectionFactory.CreateTransient(), false)
        {
            //
        }
    }
}