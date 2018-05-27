namespace ClassifiedApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyValues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropertyValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassifiedId = c.Int(nullable: false),
                        PropertyDefinitionId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classified", t => t.ClassifiedId)
                .ForeignKey("dbo.PropertyDefinition", t => t.PropertyDefinitionId)
                .Index(t => t.ClassifiedId)
                .Index(t => t.PropertyDefinitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropertyValue", "PropertyDefinitionId", "dbo.PropertyDefinition");
            DropForeignKey("dbo.PropertyValue", "ClassifiedId", "dbo.Classified");
            DropIndex("dbo.PropertyValue", new[] { "PropertyDefinitionId" });
            DropIndex("dbo.PropertyValue", new[] { "ClassifiedId" });
            DropTable("dbo.PropertyValue");
        }
    }
}
