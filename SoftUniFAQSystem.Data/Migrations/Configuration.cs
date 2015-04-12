namespace SoftUniFAQSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                Configuration.CreateTestUsers(context);
            }
        }

        private static void CreateTestUsers(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(new IdentityRole {Name = "Achkov"});
            roleManager.Create(new IdentityRole {Name = "Moderator"});
            roleManager.Create(new IdentityRole {Name = "User"});

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            //TODO: DELETE HERE WHEN DEPLOY, They're dummy accounts
            var admin = new ApplicationUser {UserName = "admin"};
            userManager.Create(admin, "Aa#123456");
            userManager.AddToRole(admin.Id, "Achkov");

            var moderator = new ApplicationUser {UserName = "moderator"};
            userManager.Create(moderator, "Aa#123456");
            userManager.AddToRole(moderator.Id, "Moderator");

            var user = new ApplicationUser {UserName = "pesho"};
            userManager.Create(user, "Aa#123456");
            userManager.AddToRole(user.Id, "User");

            var kasskata = new ApplicationUser {UserName = "kasskata"};
            userManager.Create(kasskata, "Aa#123456");
            userManager.AddToRole(kasskata.Id, "Achkov");

            var vasko = new ApplicationUser {UserName = "vasko"};
            userManager.Create(vasko, "Aa#123456");
            userManager.AddToRole(vasko.Id, "Achkov");

            var jan = new ApplicationUser {UserName = "jan"};
            userManager.Create(jan, "Aa#123456");
            userManager.AddToRole(jan.Id, "Achkov");

            var bankin = new ApplicationUser {UserName = "bankin"};
            userManager.Create(bankin, "Aa#123456");
            userManager.AddToRole(bankin.Id, "Achkov");

            context.SaveChanges();
        }
    }
}
