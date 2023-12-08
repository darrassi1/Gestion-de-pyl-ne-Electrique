namespace GESTELEC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate_db_shema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consommations",
                c => new
                    {
                        ConsommationId = c.Int(nullable: false, identity: true),
                        VolumeGasoil = c.Double(nullable: false),
                        PrixBon = c.Double(nullable: false),
                        DateRemplissage = c.DateTime(nullable: false),
                        Kilometrage = c.Double(nullable: false),
                        Immatricule = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ConsommationId)
                .ForeignKey("dbo.Vehicules", t => t.Immatricule)
                .Index(t => t.Immatricule);
            
            CreateTable(
                "dbo.Vehicules",
                c => new
                    {
                        Immatricule = c.String(nullable: false, maxLength: 128),
                        Model = c.String(nullable: false),
                        TypeCarburant = c.String(nullable: false),
                        KilometrageInitial = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Immatricule);
            
            CreateTable(
                "dbo.Ouvriers",
                c => new
                    {
                        CIN = c.String(nullable: false, maxLength: 128),
                        NomComplet = c.String(nullable: false),
                        Ville = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                        DateDebutActivite = c.DateTime(nullable: false),
                        Poste = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CIN);
            
            CreateTable(
                "dbo.Paiements",
                c => new
                    {
                        PaiementId = c.Int(nullable: false, identity: true),
                        Montant = c.Double(nullable: false),
                        DatePaiement = c.DateTime(nullable: false),
                        CIN = c.String(maxLength: 128),
                        PyloneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaiementId)
                .ForeignKey("dbo.Ouvriers", t => t.CIN)
                .ForeignKey("dbo.Pylones", t => t.PyloneId, cascadeDelete: true)
                .Index(t => t.CIN)
                .Index(t => t.PyloneId);
            
            CreateTable(
                "dbo.Pylones",
                c => new
                    {
                        PyloneId = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false),
                        LigneElectrique = c.String(nullable: false),
                        Ville = c.String(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        EtatDegradation = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PyloneId);
            
            CreateTable(
                "dbo.Repos",
                c => new
                    {
                        ReposId = c.Int(nullable: false, identity: true),
                        DateRepos = c.DateTime(nullable: false),
                        MotifRepos = c.String(nullable: false),
                        CIN = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReposId)
                .ForeignKey("dbo.Ouvriers", t => t.CIN)
                .Index(t => t.CIN);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Repos", "CIN", "dbo.Ouvriers");
            DropForeignKey("dbo.Paiements", "PyloneId", "dbo.Pylones");
            DropForeignKey("dbo.Paiements", "CIN", "dbo.Ouvriers");
            DropForeignKey("dbo.Consommations", "Immatricule", "dbo.Vehicules");
            DropIndex("dbo.Repos", new[] { "CIN" });
            DropIndex("dbo.Paiements", new[] { "PyloneId" });
            DropIndex("dbo.Paiements", new[] { "CIN" });
            DropIndex("dbo.Consommations", new[] { "Immatricule" });
            DropTable("dbo.Repos");
            DropTable("dbo.Pylones");
            DropTable("dbo.Paiements");
            DropTable("dbo.Ouvriers");
            DropTable("dbo.Vehicules");
            DropTable("dbo.Consommations");
        }
    }
}
