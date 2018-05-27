namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivationCode2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivationCode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Reason = c.String(nullable: false, maxLength: 4),
                        Code = c.String(nullable: false, maxLength: 6),
                        IssuedOn = c.DateTime(nullable: false),
                        ExpiresOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivationCode", "UserId", "dbo.User");
            DropIndex("dbo.ActivationCode", new[] { "UserId" });
            DropTable("dbo.ActivationCode");
        }
    }
}
