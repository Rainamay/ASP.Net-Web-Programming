namespace EnrollmentApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enrollments", "Grade", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enrollments", "Grade");
        }
    }
}
