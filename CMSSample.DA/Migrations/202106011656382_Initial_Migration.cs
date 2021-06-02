namespace CMSSample.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DZs",
                c => new
                    {
                        DZId = c.Int(nullable: false, identity: true),
                        DZName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.DZId);
            
            CreateTable(
                "dbo.ODZCases",
                c => new
                    {
                        ODZCaseID = c.Int(nullable: false, identity: true),
                        ODZCaseReference = c.Int(nullable: false),
                        IncidentTypeID = c.Int(nullable: false),
                        CountryofIncidentID = c.Int(nullable: false),
                        CaseCoverageAmount = c.Int(nullable: false),
                        AssistedPerson = c.String(),
                        CaseDescription = c.String(),
                    })
                .PrimaryKey(t => t.ODZCaseID)
                .ForeignKey("dbo.DZs", t => t.CountryofIncidentID, cascadeDelete: true)
                .ForeignKey("dbo.IncidentTypes", t => t.IncidentTypeID, cascadeDelete: true)
                .Index(t => t.IncidentTypeID)
                .Index(t => t.CountryofIncidentID);
            
            CreateTable(
                "dbo.IncidentTypes",
                c => new
                    {
                        IncidentTypeID = c.Int(nullable: false, identity: true),
                        IncidentTypeName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.IncidentTypeID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        Mobile = c.String(maxLength: 50),
                        DZId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.DZs", t => t.DZId, cascadeDelete: true)
                .Index(t => t.DZId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "DZId", "dbo.DZs");
            DropForeignKey("dbo.ODZCases", "IncidentTypeID", "dbo.IncidentTypes");
            DropForeignKey("dbo.ODZCases", "CountryofIncidentID", "dbo.DZs");
            DropIndex("dbo.Users", new[] { "DZId" });
            DropIndex("dbo.ODZCases", new[] { "CountryofIncidentID" });
            DropIndex("dbo.ODZCases", new[] { "IncidentTypeID" });
            DropTable("dbo.Users");
            DropTable("dbo.IncidentTypes");
            DropTable("dbo.ODZCases");
            DropTable("dbo.DZs");
        }
    }
}
