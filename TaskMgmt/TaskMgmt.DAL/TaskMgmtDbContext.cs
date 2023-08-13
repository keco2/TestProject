using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TaskMgmt.Model;
using System.Data.Entity;
using TaskMgmt.DAL.Interface;

namespace TaskMgmt.DAL
{
    public class TaskMgmtDbContext : DbContext, ITaskMgmtDbContext
    {
        public TaskMgmtDbContext() : base("name=TaskMgmtDbConnection")
        {
            MigrateDatabaseToLatest();
        }

        public TaskMgmtDbContext(System.Data.Common.DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection)
        {
            //
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskEntity>().Property(t => t.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<MaterialEntity>().Property(m => m.ManufacturerCode).IsRequired();
            modelBuilder.Entity<MaterialEntity>().Property(m => m.Price).IsRequired();
            modelBuilder.Entity<MaterialEntity>().Property(m => m.UnitOfIssue).IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<TaskMaterialUsageEntity>()
                .HasKey(u => new { u.MaterialID, u.TaskID });
            modelBuilder.Entity<TaskMaterialUsageEntity>().Property(u => u.UnitOfMeasurement)
                .HasMaxLength(10)
                .IsRequired();
            modelBuilder.Entity<TaskMaterialUsageEntity>().Property(u => u.Amount)
                .IsRequired();
        }

        /// <summary>
        /// Hint: The Enable-Migrations command in the (Nuget)PackageManager Console created the Configuration class
        /// derived from DbMigrationsConfiguration with AutomaticMigrationsEnabled = false.
        /// </summary>
        private void MigrateDatabaseToLatest()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaskMgmtDbContext, TaskMgmt.DAL.Migrations.MigConfiguration>());
        }
    }
}
