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
                .ForeignKey("dbo.DZs", t => t.CountryofIncidentID)
                .ForeignKey("dbo.IncidentTypes", t => t.IncidentTypeID)
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
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskTypeID = c.Int(nullable: false),
                        TaskDescription = c.String(),
                        ODZCaseID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CompletedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.ODZCases", t => t.ODZCaseID)
                .ForeignKey("dbo.TaskTypes", t => t.TaskTypeID)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.TaskTypeID)
                .Index(t => t.ODZCaseID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TaskTypes",
                c => new
                    {
                        TaskTypeID = c.Int(nullable: false, identity: true),
                        TaskTypeName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.TaskTypeID);
            
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
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.DZs", t => t.DZId)
                .ForeignKey("dbo.UserRoles", t => t.RoleID)
                .Index(t => t.DZId)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleID", "dbo.UserRoles");
            DropForeignKey("dbo.Users", "DZId", "dbo.DZs");
            DropForeignKey("dbo.Tasks", "TaskTypeID", "dbo.TaskTypes");
            DropForeignKey("dbo.Tasks", "ODZCaseID", "dbo.ODZCases");
            DropForeignKey("dbo.ODZCases", "IncidentTypeID", "dbo.IncidentTypes");
            DropForeignKey("dbo.ODZCases", "CountryofIncidentID", "dbo.DZs");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Users", new[] { "DZId" });
            DropIndex("dbo.Tasks", new[] { "UserId" });
            DropIndex("dbo.Tasks", new[] { "ODZCaseID" });
            DropIndex("dbo.Tasks", new[] { "TaskTypeID" });
            DropIndex("dbo.ODZCases", new[] { "CountryofIncidentID" });
            DropIndex("dbo.ODZCases", new[] { "IncidentTypeID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.TaskTypes");
            DropTable("dbo.Tasks");
            DropTable("dbo.IncidentTypes");
            DropTable("dbo.ODZCases");
            DropTable("dbo.DZs");
        }
    }
}
