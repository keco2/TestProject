namespace TaskMgmt.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Auto-generated after executing command in Nuget-PackageManagerConsole "enable-migrations"
    /// including the Initial-Create Migration script
    /// </summary>
    internal sealed class MigConfiguration : DbMigrationsConfiguration<TaskMgmt.DAL.TaskMgmtDbContext>
    {
        public MigConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TaskMgmt.DAL.TaskMgmtDbContext";
        }

        protected override void Seed(TaskMgmt.DAL.TaskMgmtDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            DbSet<TaskEntity> dbSet;
            dbSet = context.Set<TaskEntity>();
            dbSet.AddOrUpdate(new TaskEntity() { ID = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Task 1 created by Migration-Seed" });
            dbSet.AddOrUpdate(new TaskEntity() { ID = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Task 2 created by Migration-Seed" });
            dbSet.AddOrUpdate(new TaskEntity() { ID = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Task 3 created by Migration-Seed" });
            context.SaveChanges();
        }
    }
}
