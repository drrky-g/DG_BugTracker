namespace DG_BugTracker.Migrations
{
    using DG_BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DG_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DG_BugTracker.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // Instance of RoleManager 
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            // If roles do not exist, create roles
            #region Seeded Roles
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            #endregion


            //Instance of UserManager
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //If users don't exist, create these users
            #region Seeded Users
            if(!context.Users.Any(u => u.Email == "AdminAlex@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "AdminAlex@Mailinator.com",
                    Email = "AdminAlex@Mailinator.com",
                    FirstName = "Alex",
                    LastName = "Amari",
                    DisplayName = "Alex"
                }, "Admin+3775!");
            }

            if (!context.Users.Any(u => u.Email == "PMPatty@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "PMPatty@Mailinator.com",
                    Email = "PMPatty@Mailinator.com",
                    FirstName = "Patricia",
                    LastName = "Patterson",
                    DisplayName = "Patty"
                }, "PM+1117!");
            }

            if (!context.Users.Any(u => u.Email == "DevDale@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DevDale@Mailinator.com",
                    Email = "DevDale@Mailinator.com",
                    FirstName = "Dale",
                    LastName = "Daniels",
                    DisplayName = "Dale"
                }, "Dev+0627!");
            }

            if (!context.Users.Any(u => u.Email == "SubmitterSam@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "SubmitterSam@Mailinator.com",
                    Email = "SubmitterSam@Mailinator.com",
                    FirstName = "Samantha",
                    LastName = "Sadler",
                    DisplayName = "Sam"
                }, "Sub+0416!");
            }
            #endregion


            //Assign seeded users to seeded roles

            #region Role Assignments
            //set variable in first one so i can use that variable for the rest of them
            var userId = userManager.FindByEmail("AdminAlex@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("PMPatty@Mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("DevDale@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("SubmitterSam@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");
            #endregion
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
