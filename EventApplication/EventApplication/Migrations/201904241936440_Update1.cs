namespace EventApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "EventType_EventTypeId", "dbo.EventTypes");
            DropIndex("dbo.Events", new[] { "EventType_EventTypeId" });
            RenameColumn(table: "dbo.Events", name: "EventType_EventTypeId", newName: "EventTypeId");
            AlterColumn("dbo.Events", "EventTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "EventTypeId");
            AddForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes", "EventTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes");
            DropIndex("dbo.Events", new[] { "EventTypeId" });
            AlterColumn("dbo.Events", "EventTypeId", c => c.Int());
            RenameColumn(table: "dbo.Events", name: "EventTypeId", newName: "EventType_EventTypeId");
            CreateIndex("dbo.Events", "EventType_EventTypeId");
            AddForeignKey("dbo.Events", "EventType_EventTypeId", "dbo.EventTypes", "EventTypeId");
        }
    }
}
