namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNotificationCreationDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notification", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notification", "CreationDate", c => c.Boolean(nullable: false));
        }
    }
}
