namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterActivationCode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActivationCode", "Reason", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActivationCode", "Reason", c => c.String(nullable: false, maxLength: 4));
        }
    }
}
