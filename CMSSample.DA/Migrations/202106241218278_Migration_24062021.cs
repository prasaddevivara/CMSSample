namespace CMSSample.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_24062021 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ODZCases", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.ODZCases", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Tasks", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Tasks", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Users", "DeletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DeletedAt");
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.Tasks", "DeletedAt");
            DropColumn("dbo.Tasks", "IsDeleted");
            DropColumn("dbo.ODZCases", "DeletedAt");
            DropColumn("dbo.ODZCases", "IsDeleted");
        }
    }
}
