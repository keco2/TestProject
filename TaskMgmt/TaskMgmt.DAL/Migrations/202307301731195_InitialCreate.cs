namespace TaskMgmt.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskEntities",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        TotalDuration = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.TaskEntities");
        }
    }
}
