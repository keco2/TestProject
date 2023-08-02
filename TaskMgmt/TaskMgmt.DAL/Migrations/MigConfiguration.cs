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
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TaskMgmt.DAL.TaskMgmtDbContext";
        }

        protected override void Seed(TaskMgmt.DAL.TaskMgmtDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            DbSet<MaterialEntity> materialDbSet = context.Set<MaterialEntity>();
            var material1 = new MaterialEntity { ID = new Guid("7d6f2f1d-5628-4c5a-a125-3dc534b48a91"), Partnumber = "SCREW001", ManufacturerCode = 100, Price = 25, UnitOfIssue = "g" };
            var material2 = new MaterialEntity { ID = new Guid("e81d0c3b-9c16-45b1-bd63-42e9784e5f3f"), Partnumber = "OIL123", ManufacturerCode = 80, Price = 35, UnitOfIssue = "l" };
            var material3 = new MaterialEntity { ID = new Guid("c9efb625-865e-44b3-bd7e-721ef3e94738"), Partnumber = "BOLT009", ManufacturerCode = 45, Price = 10, UnitOfIssue = "pcs" };
            var material4 = new MaterialEntity { ID = new Guid("34e8c2e2-5e02-4ce1-97b1-7f4d16250a96"), Partnumber = "RADIATOR202", ManufacturerCode = 75, Price = 70, UnitOfIssue = "pcs" };
            var material5 = new MaterialEntity { ID = new Guid("fbf0c02a-125c-4b71-9e47-8a7f916f31e9"), Partnumber = "AIRFILTER087", ManufacturerCode = 60, Price = 15, UnitOfIssue = "pcs" };
            var material6 = new MaterialEntity { ID = new Guid("5da2645e-d8a9-4d8a-945b-8dfbf68d8c7f"), Partnumber = "SPARKPLUG342", ManufacturerCode = 40, Price = 12, UnitOfIssue = "pcs" };

            materialDbSet.AddOrUpdate(material1);
            materialDbSet.AddOrUpdate(material2);
            materialDbSet.AddOrUpdate(material3);
            materialDbSet.AddOrUpdate(material4);
            materialDbSet.AddOrUpdate(material5);
            materialDbSet.AddOrUpdate(material6);

            DbSet<TaskEntity> taskDbSet = context.Set<TaskEntity>();
            taskDbSet.AddOrUpdate(new TaskEntity() { ID = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Task 1 created by Migration-Seed", Material = material1, UnitOfMeasurement = "kg" });
            taskDbSet.AddOrUpdate(new TaskEntity() { ID = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Task 2 created by Migration-Seed", Material = material2, UnitOfMeasurement = "l"  });
            taskDbSet.AddOrUpdate(new TaskEntity() { ID = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Task 3 created by Migration-Seed", Material = material2, UnitOfMeasurement = "ml"  });
            taskDbSet.AddOrUpdate(new TaskEntity() { ID = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Task 4 created by Migration-Seed", Material = material2, UnitOfMeasurement = "ml"  });

            context.SaveChanges();
        }
    }
}
