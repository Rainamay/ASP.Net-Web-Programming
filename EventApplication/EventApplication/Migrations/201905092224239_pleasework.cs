namespace EventApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pleasework : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Orders");
            AddColumn("dbo.Orders", "RecordId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "RecordId");
            DropColumn("dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Orders");
            DropColumn("dbo.Orders", "RecordId");
            AddPrimaryKey("dbo.Orders", "OrderId");
        }
    }
}
