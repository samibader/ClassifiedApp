namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Type = c.Byte(nullable: false),
                        Message = c.String(nullable: false, maxLength: 100),
                        IsGeneral = c.Boolean(nullable: false),
                        CreationDate = c.Boolean(nullable: false),
                        Seen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notification", "UserId", "dbo.User");
            DropIndex("dbo.Notification", new[] { "UserId" });
            DropTable("dbo.Notification");
        }
    }
}
