namespace CMSSample.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        CaseCoverageAmount = c.Int(nullable: false),
                        AssistedPerson = c.String(),
                        CaseDescription = c.String(),
                    })
                .PrimaryKey(t => t.ODZCaseID);
            
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
            
            CreateTable(
                "dbo.ODZCase1",
                c => new
                    {
                        ODZCaseID = c.Int(nullable: false, identity: true),
                        IncidentTypeID = c.Int(nullable: false),
                        CountryofIncidentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ODZCaseID)
                .ForeignKey("dbo.IncidentTypes", t => t.IncidentTypeID, cascadeDelete: true)
                .ForeignKey("dbo.DZs", t => t.CountryofIncidentID, cascadeDelete: true)
                .Index(t => t.IncidentTypeID)
                .Index(t => t.CountryofIncidentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ODZCase1", "CountryofIncidentID", "dbo.DZs");
            DropForeignKey("dbo.ODZCase1", "IncidentTypeID", "dbo.IncidentTypes");
            DropForeignKey("dbo.Users", "DZId", "dbo.DZs");
            DropIndex("dbo.ODZCase1", new[] { "CountryofIncidentID" });
            DropIndex("dbo.ODZCase1", new[] { "IncidentTypeID" });
            DropIndex("dbo.Users", new[] { "DZId" });
            DropTable("dbo.ODZCase1");
            DropTable("dbo.Users");
            DropTable("dbo.IncidentTypes");
            DropTable("dbo.ODZCases");
            DropTable("dbo.DZs");
        }
    }
}
