namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ImageUrl = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.SubPropertyDefinition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubCategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategory", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.SubCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubPropertyDefinition", "SubCategoryId", "dbo.SubCategory");
            DropForeignKey("dbo.SubCategory", "CategoryId", "dbo.Category");
            DropIndex("dbo.SubPropertyDefinition", new[] { "SubCategoryId" });
            DropIndex("dbo.SubCategory", new[] { "CategoryId" });
            DropTable("dbo.SubPropertyDefinition");
            DropTable("dbo.SubCategory");
        }
    }
}
