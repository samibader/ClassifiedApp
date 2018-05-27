namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassifiedsV4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classified", "AdPrice", c => c.Double());
            AlterColumn("dbo.Classified", "PostingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Classified", "PostingDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Classified", "AdPrice", c => c.Double(nullable: false));
        }
    }
}
