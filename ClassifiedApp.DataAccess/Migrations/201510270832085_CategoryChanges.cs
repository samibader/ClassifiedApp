namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "ImageUrl", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Category", "Language", c => c.String(nullable: false, maxLength: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "Language", c => c.String());
            AlterColumn("dbo.Category", "ImageUrl", c => c.String());
        }
    }
}
