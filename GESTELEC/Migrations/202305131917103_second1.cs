namespace GESTELEC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        WorkId = c.Int(nullable: false, identity: true),
                        OuvrierCIN = c.String(nullable: false, maxLength: 128),
                        PyloneNumero = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Pylone_PyloneId = c.Int(),
                    })
                .PrimaryKey(t => t.WorkId)
                .ForeignKey("dbo.Ouvriers", t => t.OuvrierCIN, cascadeDelete: true)
                .ForeignKey("dbo.Pylones", t => t.Pylone_PyloneId)
                .Index(t => t.OuvrierCIN)
                .Index(t => t.Pylone_PyloneId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works", "Pylone_PyloneId", "dbo.Pylones");
            DropForeignKey("dbo.Works", "OuvrierCIN", "dbo.Ouvriers");
            DropIndex("dbo.Works", new[] { "Pylone_PyloneId" });
            DropIndex("dbo.Works", new[] { "OuvrierCIN" });
            DropTable("dbo.Works");
        }
    }
}
