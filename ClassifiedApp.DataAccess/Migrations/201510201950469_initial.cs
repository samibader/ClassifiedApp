namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AuthToken = c.Guid(nullable: false),
                        IssuedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        HashedPassword = c.String(nullable: false, maxLength: 200),
                        Salt = c.String(nullable: false, maxLength: 200),
                        GSM = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 100),
                        Country = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        IsEmailApproved = c.Boolean(nullable: false),
                        IsGsmApproved = c.Boolean(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        IsGpsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "UserId", "dbo.User");
            DropIndex("dbo.Token", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Token");
            DropTable("dbo.Category");
        }
    }
}
