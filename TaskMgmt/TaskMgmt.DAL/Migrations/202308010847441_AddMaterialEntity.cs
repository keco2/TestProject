namespace TaskMgmt.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddMaterialEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaterialEntities",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Partnumber = c.String(),
                        ManufacturerCode = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        UnitOfIssue = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("dbo.MaterialEntities");
        }
    }
}
