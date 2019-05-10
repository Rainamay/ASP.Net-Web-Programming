namespace EventApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Orders");
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "RecordId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "RecordId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "OrderId");
            AddPrimaryKey("dbo.Orders", "RecordId");
        }
    }
}
