namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassifiedsV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FavoriteSenderId = c.Int(nullable: false),
                        FavoriteReceiverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.FavoriteReceiverId)
                .ForeignKey("dbo.User", t => t.FavoriteSenderId)
                .Index(t => t.FavoriteSenderId)
                .Index(t => t.FavoriteReceiverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteUser", "FavoriteSenderId", "dbo.User");
            DropForeignKey("dbo.FavoriteUser", "FavoriteReceiverId", "dbo.User");
            DropIndex("dbo.FavoriteUser", new[] { "FavoriteReceiverId" });
            DropIndex("dbo.FavoriteUser", new[] { "FavoriteSenderId" });
            DropTable("dbo.FavoriteUser");
        }
    }
}
