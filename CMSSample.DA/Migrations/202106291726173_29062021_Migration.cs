namespace CMSSample.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29062021_Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreationDate");
        }
    }
}
