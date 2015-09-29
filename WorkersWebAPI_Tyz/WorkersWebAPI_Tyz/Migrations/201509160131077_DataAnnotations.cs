namespace WorkersWebAPI_Tyz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Workers", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Workers", "Department", c => c.String(nullable: false));
            AlterColumn("dbo.Workers", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.Workers", "EmployedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workers", "EmployedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Workers", "Position", c => c.String());
            AlterColumn("dbo.Workers", "Department", c => c.String());
            AlterColumn("dbo.Workers", "Gender", c => c.String());
            AlterColumn("dbo.Workers", "Name", c => c.String());
        }
    }
}
