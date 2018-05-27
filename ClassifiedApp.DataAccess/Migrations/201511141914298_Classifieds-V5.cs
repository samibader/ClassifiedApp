namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassifiedsV5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassifiedImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassifiedId = c.Int(nullable: false),
                        ImageGuid = c.String(nullable: false, maxLength: 200),
                        ImageExtension = c.String(nullable: false, maxLength: 5),
                        IsMainImage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classified", t => t.ClassifiedId, cascadeDelete: true)
                .Index(t => t.ClassifiedId);
            
            DropColumn("dbo.Classified", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classified", "ImageUrl", c => c.String(maxLength: 200));
            DropForeignKey("dbo.ClassifiedImage", "ClassifiedId", "dbo.Classified");
            DropIndex("dbo.ClassifiedImage", new[] { "ClassifiedId" });
            DropTable("dbo.ClassifiedImage");
        }
    }
}
