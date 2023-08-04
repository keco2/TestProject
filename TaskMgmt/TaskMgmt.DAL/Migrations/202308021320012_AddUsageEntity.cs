namespace TaskMgmt.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddUsageEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskMaterialUsageEntities",
                c => new
                    {
                        MaterialID = c.Guid(nullable: false),
                        TaskID = c.Guid(nullable: false),
                        Amount = c.Int(nullable: false),
                        UnitOfMeasurement = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => new { t.MaterialID, t.TaskID })
                .ForeignKey("dbo.MaterialEntities", t => t.MaterialID, cascadeDelete: true)
                .ForeignKey("dbo.TaskEntities", t => t.TaskID, cascadeDelete: true)
                .Index(t => t.MaterialID)
                .Index(t => t.TaskID);

            AlterColumn("dbo.MaterialEntities", "UnitOfIssue", c => c.String(nullable: false, maxLength: 10));
        }

        public override void Down()
        {
            DropForeignKey("dbo.TaskMaterialUsageEntities", "TaskID", "dbo.TaskEntities");
            DropForeignKey("dbo.TaskMaterialUsageEntities", "MaterialID", "dbo.MaterialEntities");
            DropIndex("dbo.TaskMaterialUsageEntities", new[] { "TaskID" });
            DropIndex("dbo.TaskMaterialUsageEntities", new[] { "MaterialID" });
            AlterColumn("dbo.MaterialEntities", "UnitOfIssue", c => c.String(nullable: false));
            DropTable("dbo.TaskMaterialUsageEntities");
        }
    }
}
