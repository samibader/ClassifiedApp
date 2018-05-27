namespace ClassifiedApp.DataAccess.Migrations
{
    using ClassifiedApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClassifiedApp.DataAccess.ClassifiedAppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ClassifiedApp.DataAccess.ClassifiedAppDbContext context)
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
            context.Admins.AddOrUpdate(
                new Models.Admin()
                    {
                        Email = "webadmin@classifiedapp.com",
                        Username = "webadmin",
                        Salt = "DzXmZ1PhPNOc+eY+V6i/jA==",
                        HashedPassword = "6l6Vxi/WHlGc/SzJV4QLhny3Bu8="
                    }
                );
            //context.Categories.AddOrUpdate(new Category()
            //    {
            //        ImageUrl = "placeholder.jpg",
            //        Language = "en",
            //        Name = "Cat1"
            //    });
            //context.Categories.AddOrUpdate(new Category()
            //{
            //    ImageUrl = "placeholder.jpg",
            //    Language = "en",
            //    Name = "Cat2"
            //});
            //context.Categories.AddOrUpdate(new Category()
            //{
            //    ImageUrl = "placeholder.jpg",
            //    Language = "ar",
            //    Name = "CatArabic1"
            //});
            //context.Categories.AddOrUpdate(new Category()
            //{
            //    ImageUrl = "placeholder.jpg",
            //    Language = "ar",
            //    Name = "CatArabic2"
            //});
        }
    }
}
