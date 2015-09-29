namespace WorkersWebAPI_Tyz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.String(),
                        Age = c.Byte(nullable: false),
                        Department = c.String(),
                        Position = c.String(),
                        EmployedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Workers");
        }
    }
}
