namespace EventApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "StartTime");
            DropColumn("dbo.Events", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
