namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDevice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UDID = c.String(nullable: false, maxLength: 200),
                        IsEnabled = c.Boolean(nullable: false),
                        IsAndroid = c.Boolean(nullable: false),
                        Lang = c.String(nullable: false, maxLength: 2),
                        RegistrationId = c.String(nullable: false, maxLength: 200),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Device", "UserId", "dbo.User");
            DropIndex("dbo.Device", new[] { "UserId" });
            DropTable("dbo.Device");
        }
    }
}
