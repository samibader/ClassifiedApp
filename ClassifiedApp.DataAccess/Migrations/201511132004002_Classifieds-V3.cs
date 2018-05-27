namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassifiedsV3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classified", "Status", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classified", "Status");
        }
    }
}
