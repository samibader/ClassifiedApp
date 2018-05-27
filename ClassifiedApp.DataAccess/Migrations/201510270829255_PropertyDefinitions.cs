namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyDefinitions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropertyDefinition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.Category", "ImageUrl", c => c.String());
            AddColumn("dbo.Category", "Language", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropertyDefinition", "CategoryId", "dbo.Category");
            DropIndex("dbo.PropertyDefinition", new[] { "CategoryId" });
            DropColumn("dbo.Category", "Language");
            DropColumn("dbo.Category", "ImageUrl");
            DropTable("dbo.PropertyDefinition");
        }
    }
}
