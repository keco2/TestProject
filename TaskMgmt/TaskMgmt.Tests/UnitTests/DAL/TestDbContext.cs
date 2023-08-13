using System.Data.Common;
using TaskMgmt.DAL.Interface;

namespace TaskMgmt.DAL.UnitTests
{
    public class TestDbContext : TaskMgmtDbContext, ITaskMgmtDbContext
    {
        public TestDbContext() : base(Effort.DbConnectionFactory.CreateTransient(), false)
        {
            //
        }
    }
}