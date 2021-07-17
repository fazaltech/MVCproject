namespace MVCproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xyz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblroles", "flag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblroles", "flag");
        }
    }
}
