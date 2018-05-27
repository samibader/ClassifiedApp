namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClassifiedId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Value = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classified", t => t.ClassifiedId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClassifiedId);
            
            AddColumn("dbo.FavoriteClassified", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.FavoriteUser", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rate", "UserId", "dbo.User");
            DropForeignKey("dbo.Rate", "ClassifiedId", "dbo.Classified");
            DropIndex("dbo.Rate", new[] { "ClassifiedId" });
            DropIndex("dbo.Rate", new[] { "UserId" });
            DropColumn("dbo.FavoriteUser", "CreationDate");
            DropColumn("dbo.FavoriteClassified", "CreationDate");
            DropTable("dbo.Rate");
        }
    }
}
