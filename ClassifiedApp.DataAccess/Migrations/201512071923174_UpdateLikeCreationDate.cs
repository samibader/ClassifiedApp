namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLikeCreationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Like", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Like", "CreationDate");
        }
    }
}
