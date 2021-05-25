namespace CMSSample.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_26052021 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ODZCase1", new[] { "IncidentTypeID" });
            DropIndex("dbo.ODZCase1", new[] { "CountryofIncidentID" });
            AddColumn("dbo.ODZCases", "IncidentTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.ODZCases", "CountryofIncidentID", c => c.Int(nullable: false));
            CreateIndex("dbo.ODZCases", "IncidentTypeID");
            CreateIndex("dbo.ODZCases", "CountryofIncidentID");
            DropTable("dbo.ODZCase1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ODZCase1",
                c => new
                    {
                        ODZCaseID = c.Int(nullable: false, identity: true),
                        IncidentTypeID = c.Int(nullable: false),
                        CountryofIncidentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ODZCaseID);
            
            DropIndex("dbo.ODZCases", new[] { "CountryofIncidentID" });
            DropIndex("dbo.ODZCases", new[] { "IncidentTypeID" });
            DropColumn("dbo.ODZCases", "CountryofIncidentID");
            DropColumn("dbo.ODZCases", "IncidentTypeID");
            CreateIndex("dbo.ODZCase1", "CountryofIncidentID");
            CreateIndex("dbo.ODZCase1", "IncidentTypeID");
        }
    }
}
