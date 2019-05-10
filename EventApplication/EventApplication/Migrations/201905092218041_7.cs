namespace EventApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "RecordNum", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "RecordId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "RecordId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "RecordNum");
        }
    }
}
