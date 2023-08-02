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
    public class TaskMgmtDbContext : DbContext
    {
        public TaskMgmtDbContext() : base("name=TaskMgmtDbConnection")
        {
            MigrateDatabaseToLatest();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskEntity>().Property(t => t.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<TaskEntity>().HasRequired<MaterialEntity>(k => k.Material).WithRequiredDependent().WillCascadeOnDelete();

            modelBuilder.Entity<MaterialEntity>().Property(m => m.ManufacturerCode).IsRequired();
            modelBuilder.Entity<MaterialEntity>().Property(m => m.Price).IsRequired();
            modelBuilder.Entity<MaterialEntity>().Property(m => m.UnitOfIssue).IsRequired();
            modelBuilder.Entity<MaterialEntity>().HasMany<TaskEntity>(x => x.Tasks).WithOptional();

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
        public virtual DbSet<MaterialEntity> Materials { get; set; }

    }
}
