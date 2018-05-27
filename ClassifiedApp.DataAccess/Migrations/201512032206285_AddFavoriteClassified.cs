namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavoriteClassified : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteClassified",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClassifiedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classified", t => t.ClassifiedId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ClassifiedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteClassified", "UserId", "dbo.User");
            DropForeignKey("dbo.FavoriteClassified", "ClassifiedId", "dbo.Classified");
            DropIndex("dbo.FavoriteClassified", new[] { "ClassifiedId" });
            DropIndex("dbo.FavoriteClassified", new[] { "UserId" });
            DropTable("dbo.FavoriteClassified");
        }
    }
}
