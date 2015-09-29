namespace WorkersWebAPI_Tyz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tyz : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workers", "Name", c => c.String(nullable: false, maxLength: 18));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workers", "Name", c => c.String(nullable: false));
        }
    }
}
