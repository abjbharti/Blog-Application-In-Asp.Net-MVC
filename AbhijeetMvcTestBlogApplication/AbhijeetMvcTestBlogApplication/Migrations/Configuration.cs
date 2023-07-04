namespace AbhijeetMvcTestBlogApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AbhijeetMvcTestBlogApplication.Models.BlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AbhijeetMvcTestBlogApplication.Models.BlogDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Users.AddOrUpdate(user => user.UserId, new Models.User() { UserName = "admin", Password = "admin", Role = "admin" },
                new Models.User()
                {
                    UserName = "abhijeet",
                    Password = "abhijeet",
                    Role = "admin"
                },
                new Models.User()
                {
                    UserName = "user",
                    Password = "user",
                    Role = "user"
                },
                new Models.User() { UserName = "bharti", Password = "bharti", Role = "user" }
            );
        }
    }
}
