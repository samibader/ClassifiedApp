namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Testing",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        vvvv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Testing");
        }
    }
}
