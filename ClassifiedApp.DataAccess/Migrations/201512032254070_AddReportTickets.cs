namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReportTickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportClassifiedTicket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReporterUserId = c.Int(nullable: false),
                        ClassifiedId = c.Int(nullable: false),
                        ReportReason = c.Byte(nullable: false),
                        Note = c.String(),
                        IssuedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classified", t => t.ClassifiedId)
                .ForeignKey("dbo.User", t => t.ReporterUserId)
                .Index(t => t.ReporterUserId)
                .Index(t => t.ClassifiedId);
            
            CreateTable(
                "dbo.ReportUserTicket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReporterUserId = c.Int(nullable: false),
                        ReportedUserId = c.Int(nullable: false),
                        ReportReason = c.Byte(nullable: false),
                        Note = c.String(),
                        IssuedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ReportedUserId)
                .ForeignKey("dbo.User", t => t.ReporterUserId)
                .Index(t => t.ReporterUserId)
                .Index(t => t.ReportedUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportUserTicket", "ReporterUserId", "dbo.User");
            DropForeignKey("dbo.ReportUserTicket", "ReportedUserId", "dbo.User");
            DropForeignKey("dbo.ReportClassifiedTicket", "ReporterUserId", "dbo.User");
            DropForeignKey("dbo.ReportClassifiedTicket", "ClassifiedId", "dbo.Classified");
            DropIndex("dbo.ReportUserTicket", new[] { "ReportedUserId" });
            DropIndex("dbo.ReportUserTicket", new[] { "ReporterUserId" });
            DropIndex("dbo.ReportClassifiedTicket", new[] { "ClassifiedId" });
            DropIndex("dbo.ReportClassifiedTicket", new[] { "ReporterUserId" });
            DropTable("dbo.ReportUserTicket");
            DropTable("dbo.ReportClassifiedTicket");
        }
    }
}
