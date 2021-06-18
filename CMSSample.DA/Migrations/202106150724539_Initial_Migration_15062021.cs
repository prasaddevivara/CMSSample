namespace CMSSample.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration_15062021 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Tasks", "CompletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "CompletedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tasks", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
