using TaskMgmt.DAL.Interface;
using System.Data.Common;

namespace TaskMgmt.DAL.UnitTests
{
    public class TestContext : TaskMgmtDbContext
    {
        public TestContext(DbConnection connection) : base(connection, false)
        {
            //
        }
    }
}