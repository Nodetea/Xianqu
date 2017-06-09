namespace Xianqu.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Xianqu.Web.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<Xianqu.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Xianqu.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(Xianqu.Web.Models.ApplicationDbContext context)
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
            context.UserRoles.AddOrUpdate(x => x.Id,
                new ApplicationRole() { Id = "1", Name = "用户" },
                new ApplicationRole() { Id="2",Name="商家"}
                );
        }
    }
}
