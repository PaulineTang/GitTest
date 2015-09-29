namespace WorkersWebAPI_Tyz.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WorkersWebAPI_Tyz.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<WorkersWebAPI_Tyz.Models.WorkerDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WorkersWebAPI_Tyz.Models.WorkerDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Workers.AddOrUpdate(i => i.Name,
        new Worker

        {
            Id = 20150230,
            Name = "Sam Tang",
            Gender = "male",
            Age = 25,
            Department = "JS",
            Position = "staff",
            EmployedDate = DateTime.Parse("2015-07-10")
        },
         new Worker
         {
             Id = 20118965,
             Name = "Tom Tang",
             Gender = "male",
             Age = 25,
             Department = "JS",
             Position = "director",
             EmployedDate = DateTime.Parse("2015-07-22")
         },
         new Worker
         {
             Id = 20150001,
             Name = "Pauline Tang",
             Gender = "female",
             Age = 25,
             Department = "JS",
             Position = "staff",
             EmployedDate = DateTime.Parse("2015-07-22")
         },
            new Worker
            {
                Id = 20140078,
                Name = "Jim Green",
                Gender = "male",
                Age = 35,
                Department = "RF",
                Position = "staff",
                EmployedDate = DateTime.Parse("2010-07-22")
            },
            new Worker
            {
                Id = 20110000,
                Name = "Lily Green",
                Gender = "female",
                Age = 20,
                Department = "CS",
                Position = "staff",
                EmployedDate = DateTime.Parse("2014-05-22")
            },
            new Worker
            {
                Id = 20110050,
                Name = "Lucy Green",
                Gender = "female",
                Age = 20,
                Department = "CS",
                Position = "manager",
                EmployedDate = DateTime.Parse("2014-05-22")
            }
         );
        }
    }
}
