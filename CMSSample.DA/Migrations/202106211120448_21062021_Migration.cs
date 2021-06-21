namespace CMSSample.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21062021_Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ODZCases", "CaseCreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ODZCases", "ValidatedByUser", c => c.Int());
            AddColumn("dbo.ODZCases", "ValidationDate", c => c.DateTime());
            AddColumn("dbo.ODZCases", "ValidationDesc", c => c.String());
            AddColumn("dbo.ODZCases", "ClosedByuser", c => c.Int());
            AddColumn("dbo.ODZCases", "ClosedByDate", c => c.DateTime());
            AddColumn("dbo.ODZCases", "ClosingDesc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ODZCases", "ClosingDesc");
            DropColumn("dbo.ODZCases", "ClosedByDate");
            DropColumn("dbo.ODZCases", "ClosedByuser");
            DropColumn("dbo.ODZCases", "ValidationDesc");
            DropColumn("dbo.ODZCases", "ValidationDate");
            DropColumn("dbo.ODZCases", "ValidatedByUser");
            DropColumn("dbo.ODZCases", "CaseCreationDate");
        }
    }
}
