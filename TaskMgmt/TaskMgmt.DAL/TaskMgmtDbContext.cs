using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TaskMgmt.Model;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace TaskMgmt.DAL
{

    internal sealed class Configuration : DbMigrationsConfiguration<TaskMgmt.DAL.TaskMgmtDbContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            //ContextKey = "TaskMgmt.DAL.TaskMgmtDbContext";
        }

        protected override void Seed(TaskMgmt.DAL.TaskMgmtDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }


    public class TaskMgmtDbContext : DbContext
    {
        public TaskMgmtDbContext() : base("name=ccc")
        {
            MigrateDatabaseToLatest();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskEntity>().Property(c => c.Name).IsRequired()
                                                               .HasMaxLength(255);
            // Zero or One to Many
            //modelBuilder.Entity<TaskMaterialUsage>().HasOptional(a => a.Country)
            //                               .WithMany(c => c.Airplanes)
            //                               .HasForeignKey(a => a.CountryId);

            // Add complex type Configuration Classes
            //modelBuilder.Configurations.Add(new TaskMgmtDbConfig());
        }

        /// <summary>
        /// Hint: The Enable-Migrations command in the (Nuget)PackageManager Console created the Configuration class
        /// derived from DbMigrationsConfiguration with AutomaticMigrationsEnabled = false.
        /// </summary>
        private void MigrateDatabaseToLatest()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaskMgmtDbContext, TaskMgmt.DAL.Migrations.MigConfiguration>());
        }

        public virtual DbSet<TaskEntity> Tasks { get; set; }

    }
}
