namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeedbackTicket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedbackTicket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Note = c.String(maxLength: 500),
                        IssuedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.ReportClassifiedTicket", "Note", c => c.String(maxLength: 500));
            AlterColumn("dbo.ReportUserTicket", "Note", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeedbackTicket", "UserId", "dbo.User");
            DropIndex("dbo.FeedbackTicket", new[] { "UserId" });
            AlterColumn("dbo.ReportUserTicket", "Note", c => c.String());
            AlterColumn("dbo.ReportClassifiedTicket", "Note", c => c.String());
            DropTable("dbo.FeedbackTicket");
        }
    }
}
