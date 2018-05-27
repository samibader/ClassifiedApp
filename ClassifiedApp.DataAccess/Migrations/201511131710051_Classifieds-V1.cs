namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassifiedsV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classified",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                        AdType = c.Byte(nullable: false),
                        AdPrice = c.Double(nullable: false),
                        Country = c.String(nullable: false, maxLength: 50),
                        City = c.String(maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false),
                        AddingDate = c.DateTime(nullable: false),
                        PostingDate = c.DateTime(nullable: false),
                        Notes = c.String(maxLength: 500),
                        IsFeatured = c.Boolean(nullable: false),
                        ImageUrl = c.String(maxLength: 200),
                        ViewCount = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategory", t => t.SubCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SubCategoryId);
            
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClassifiedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classified", t => t.ClassifiedId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClassifiedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classified", "UserId", "dbo.User");
            DropForeignKey("dbo.Classified", "SubCategoryId", "dbo.SubCategory");
            DropForeignKey("dbo.Like", "UserId", "dbo.User");
            DropForeignKey("dbo.Like", "ClassifiedId", "dbo.Classified");
            DropIndex("dbo.Like", new[] { "ClassifiedId" });
            DropIndex("dbo.Like", new[] { "UserId" });
            DropIndex("dbo.Classified", new[] { "SubCategoryId" });
            DropIndex("dbo.Classified", new[] { "UserId" });
            DropTable("dbo.Like");
            DropTable("dbo.Classified");
        }
    }
}
