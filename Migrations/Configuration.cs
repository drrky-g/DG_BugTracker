namespace DG_BugTracker.Migrations
{
    using DG_BugTracker.Helpers;
    using DG_BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

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

            //Instance of ProjectHelper
            var projectHelper = new ProjectHelper();
            var userProjectsHelper = new UserProjectsHelper();
            var userRoleHelper = new UserRoleHelper();

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

            //Avengers Users Seed

            //Admin
            //-----------------------
            //Derrick (Derrick Gordon) created
            //Fury (Nick Fury) created image-fury

            //Project Managers
            //-----------------------

            //Iron Man (Tony Stark) created image-ironman
            //Captain America (Steve Roders) created image-cap
            //Black Widow (Natasha Romanoff) created
            //Captain Marvel (Carol Danvers) created
            //Black Panther (T'Challa T'Chaka) created
            //Thor (Thor Odinson) created

            //Developers
            //------------------------

            //Hulk (Bruce Banner) created
            //Vision (Victor Shade) created
            //Ant Man (Scott Lang) created
            //Dr. Strange (Steven Strange) created
            //Rocket (Rocket Raccoon) created
            //Shuri (Shuri T'Chaka) created

            //Submitters
            //--------------------------

            //Hawkeye (Clint Barton) created
            //Spider Man (Peter Parker) created
            //War Machine (James Rhodes) created
            //Star Lord (Peter Quill) created
            //Mantis (Mandy Celestine) created
            //Scarlet Witch (Wanda Maximoff) created
            //Winter Soldier (James Buchanan) created
            //Pepper (Virginia Potts) created
            //Falcon (Sam Wilson)

            //passwords
            #region Passwords from private.config
            var myPassword = WebConfigurationManager.AppSettings["myaccountpassword"];
            var demoPassword = WebConfigurationManager.AppSettings["demopassword"];
            var avengerPassword = WebConfigurationManager.AppSettings["avengerspassword"];
            #endregion

            //Add Avengers User Accounts (still need to add avatars)
            #region Add Avengers as users.
            if (!context.Users.Any(u => u.Email == "derrickwg17@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "themaster",
                    UserName = "derrickwg17@gmail.com",
                    Email = "derrickwg17@gmail.com",
                    FirstName = "Derrick",
                    LastName = "Gordon",
                    DisplayName = "Derrick"
                }, myPassword);
            }

            if (!context.Users.Any(u => u.Email == "Fury@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "nickfury",
                    UserName = "Fury@Mailinator.com",
                    Email = "Fury@Mailinator.com",
                    FirstName = "Nick",
                    LastName = "Fury",
                    DisplayName = "Fury"
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == "IronMan@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "ironman",
                    UserName = "IronMan@Mailinator.com",
                    Email = "IronMan@Mailinator.com",
                    FirstName = "Tony",
                    LastName = "Stark",
                    DisplayName = "Iron Man"
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == "CaptainAmerica@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "captamerica",
                    UserName = "CaptainAmerica@Mailinator.com",
                    Email = "CaptainAmerica@Mailinator.com",
                    FirstName = "Steve",
                    LastName = "Rogers",
                    DisplayName = "Captain America"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "BlackWidow@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "blackwidow",
                    UserName = "BlackWidow@Mailinator.com",
                    Email = "BlackWidow@Mailinator.com",
                    FirstName = "Natasha",
                    LastName = "Romanov",
                    DisplayName = "Black Widow"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "CaptainMarvel@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "captmarvel",
                    UserName = "CaptainMarvel@Mailinator.com",
                    Email = "CaptainMarvel@Mailinator.com",
                    FirstName = "Carol",
                    LastName = "Danvers",
                    DisplayName = "Captain Marvel"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "Thor@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "thorodinson",
                    UserName = "Thor@Mailinator.com",
                    Email = "Thor@Mailinator.com",
                    FirstName = "Thor",
                    LastName = "Odinson",
                    DisplayName = "Thor"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "BlackPanther@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "blackpanther",
                    UserName = "BlackPanther@Mailinator.com",
                    Email = "BlackPanther@Mailinator.com",
                    FirstName = "T'Challa",
                    LastName = "T'Chaka",
                    DisplayName = "Black Panther"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "Hulk@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "thehulk",
                    UserName = "Hulk@Mailinator.com",
                    Email = "Hulk@Mailinator.com",
                    FirstName = "Bruce",
                    LastName = "Banner",
                    DisplayName = "Hulk"
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == "Vision@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "vision",
                    UserName = "Vision@Mailinator.com",
                    Email = "Vision@Mailinator.com",
                    FirstName = "Victor",
                    LastName = "Shade",
                    DisplayName = "Vision"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "AntMan@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "antman",
                    UserName = "AntMan@Mailinator.com",
                    Email = "AntMan@Mailinator.com",
                    FirstName = "Scott",
                    LastName = "Lang",
                    DisplayName = "AntMan"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "DrStrange@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "drstrange",
                    UserName = "DrStrange@Mailinator.com",
                    Email = "DrStrange@Mailinator.com",
                    FirstName = "Steven",
                    LastName = "Strange",
                    DisplayName = "Dr. Strange"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "Rocket@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "rocket",
                    UserName = "Rocket@Mailinator.com",
                    Email = "Rocket@Mailinator.com",
                    FirstName = "Rocket",
                    LastName = "Raccoon",
                    DisplayName = "Rocket"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "Shuri@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "shuri",
                    UserName = "Shuri@Mailinator.com",
                    Email = "Shuri@Mailinator.com",
                    FirstName = "Shuri",
                    LastName = "T'Chaka",
                    DisplayName = "Shuri"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "Hawkeye@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "hawkeye",
                    UserName = "Hawkeye@Mailinator.com",
                    Email = "Hawkeye@Mailinator.com",
                    FirstName = "Clint",
                    LastName = "Barton",
                    DisplayName = "Hawkeye"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "SpiderMan@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "spiderman",
                    UserName = "SpiderMan@Mailinator.com",
                    Email = "SpiderMan@Mailinator.com",
                    FirstName = "Peter",
                    LastName = "Parker",
                    DisplayName = "Spider Man"
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == "StarLord@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "starlord",
                    UserName = "StarLord@Mailinator.com",
                    Email = "StarLord@Mailinator.com",
                    FirstName = "Peter",
                    LastName = "Quill",
                    DisplayName = "Star Lord"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "WarMachine@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "warmachine",
                    UserName = "WarMachine@Mailinator.com",
                    Email = "WarMachine@Mailinator.com",
                    FirstName = "James",
                    LastName = "Rhodes",
                    DisplayName = "War Machine"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "Mantis@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "mantis",
                    UserName = "Mantis@Mailinator.com",
                    Email = "Mantis@Mailinator.com",
                    FirstName = "Mandy",
                    LastName = "Celestine",
                    DisplayName = "Mantis"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "ScarletWitch@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "scarletwitch",
                    UserName = "ScarletWitch@Mailinator.com",
                    Email = "ScarletWitch@Mailinator.com",
                    FirstName = "Wanda",
                    LastName = "Maximoff",
                    DisplayName = "Scarlet Witch"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "WinterSoldier@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "wintersoldier",
                    UserName = "WinterSoldier@Mailinator.com",
                    Email = "WinterSoldier@Mailinator.com",
                    FirstName = "James",
                    LastName = "Buchanan",
                    DisplayName = "Winter Soldier"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "Pepper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "pepperpotts",
                    UserName = "Pepper@Mailinator.com",
                    Email = "Pepper@Mailinator.com",
                    FirstName = "Virginia",
                    LastName = "Potts",
                    DisplayName = "Pepper"
                }, avengerPassword);
            }

            if (!context.Users.Any(u => u.Email == "Falcon@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Id = "falcon",
                    UserName = "Falcon@Mailinator.com",
                    Email = "Falcon@Mailinator.com",
                    FirstName = "Sam",
                    LastName = "Wilson",
                    DisplayName = "Falcon"
                }, avengerPassword);
            }
            context.SaveChanges();
            #endregion


            //Assign Avengers their roles
            #region Assign Avengers roles
            //Admins
            var userId = userManager.FindByEmail("derrickwg17@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("Fury@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            //PMs
            userId = userManager.FindByEmail("IronMan@Mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("CaptainAmerica@Mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("BlackWidow@Mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("CaptainMarvel@Mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("BlackPanther@Mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("Thor@Mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            //Devs
            userId = userManager.FindByEmail("Hulk@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("Vision@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("AntMan@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("DrStrange@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("Rocket@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("Shuri@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            //Submitters
            userId = userManager.FindByEmail("Hawkeye@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("SpiderMan@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("WarMachine@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("StarLord@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("Mantis@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("ScarletWitch@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("WinterSoldier@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("Pepper@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("Falcon@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            context.SaveChanges();
            #endregion

            //Seed the models below, if you want to wipe and restart your database//
            #region Seeded Projects
            context.Projects.AddOrUpdate(
                project => project.Name,
                new Project { Id = 1, Name = "Bug Tracker", Description = "Debugging application to track and manage a software teams debugging efforts.", Created = DateTimeOffset.Now},
                new Project { Id = 2, Name = "Drrk.io", Description = "Portfolio website of Derrick Gordon, an ASP .NET Developer.", Created = DateTimeOffset.Now },
                new Project { Id = 3, Name = "Blog Website", Description = "A personal blog for Derrick Gordon.", Created = DateTimeOffset.Now },
                new Project { Id = 4, Name = "Mythos Website", Description = "An educational website about mythology.", Created = DateTimeOffset.Now }
                );
            context.SaveChanges();
            #endregion

            #region Project Assignments
            // assign each id to a variable
            var blogId = context.Projects.FirstOrDefault(proj => proj.Name == "Blog Website").Id;
            var trackerId = context.Projects.FirstOrDefault(proj => proj.Name == "Bug Tracker").Id;
            var portfolioId = context.Projects.FirstOrDefault(proj => proj.Name == "Drrk.io").Id;
            var mythosId = context.Projects.FirstOrDefault(proj => proj.Name == "Mythos Website").Id;

            //Project assignments:
            // Blog : scarletwitch, warmachine, thehulk, ironman
            projectHelper.AddUserToProject("scarletwitch", blogId);
            projectHelper.AddUserToProject("warmachine", blogId);
            projectHelper.AddUserToProject("thehulk", blogId);
            projectHelper.AddUserToProject("ironman", blogId);

            // Bugtracker : spiderman, starlord, drstrange, antman, blackwidow
            projectHelper.AddUserToProject("spiderman", trackerId);
            projectHelper.AddUserToProject("starlord", trackerId);
            projectHelper.AddUserToProject("drstrange", trackerId);
            projectHelper.AddUserToProject("antman", trackerId);
            projectHelper.AddUserToProject("blackwidow", trackerId);

            // Mythos : mantis, rocket, thorodinson
            projectHelper.AddUserToProject("mantis", portfolioId);
            projectHelper.AddUserToProject("rocket", portfolioId);
            projectHelper.AddUserToProject("thorodinson", portfolioId);

            // Portfolio : falcon, pepperpotts, vision, shuri, blackpanther
            projectHelper.AddUserToProject("falcon", mythosId);
            projectHelper.AddUserToProject("vision", mythosId);
            projectHelper.AddUserToProject("pepperpotts", mythosId);
            projectHelper.AddUserToProject("blackpanther", mythosId);



            #endregion


            #region  Seeded Ticket Types
            context.TicketTypes.AddOrUpdate(
                type => type.Name,
                new TicketType { Id = 1, Name = "Bug/Defect" },
                new TicketType { Id = 2, Name = "CSS/Javascript Issue" },
                new TicketType { Id = 3, Name = "Controller Issue" },
                new TicketType { Id = 4, Name = "View Issue" },
                new TicketType { Id = 5, Name = "ViewModel Issue" },
                new TicketType { Id = 6, Name = "Model Issue" },
                new TicketType { Id = 7, Name = "Documentation Request" },
                new TicketType { Id = 8, Name = "Feature Request" }
                );
            context.SaveChanges();
            #endregion

            #region Seeded Ticket Priorities
            context.TicketPriorities.AddOrUpdate(
                prior => prior.Name,
                new TicketPriority { Id = 1, Name = "Low" },
                new TicketPriority { Id = 2, Name = "Medium" },
                new TicketPriority { Id = 3, Name = "High" },
                new TicketPriority { Id = 4, Name = "Urgent" }
                );
            context.SaveChanges();
            #endregion

            #region Seeded Ticket Statuses
            context.TicketStatuses.AddOrUpdate(
                status => status.Name,
                new TicketStatus { Id = 1, Name = "New" },
                new TicketStatus { Id = 2, Name = "Assigned" },
                new TicketStatus { Id = 3, Name = "In Progress" },
                new TicketStatus { Id = 4, Name = "On Hold" },
                new TicketStatus { Id = 5, Name = "Ready for Review" },
                new TicketStatus { Id = 6, Name = "Archived" }
                );
            context.SaveChanges();
            #endregion


            #region Seeded Tickets 
            context.Tickets.AddOrUpdate(
                ticket => ticket.Title,
                /* bugtracker ticket */ new Ticket { Id = 1, Title = "Notification Dropdown", Description = "Set CSS for Notification drop down styling", Created = DateTimeOffset.Now, TicketTypeId = 2, ProjectId = 1, TicketPriorityId = 2, TicketStatusId = 2, OwnerUserId = "spiderman", AssignedToUserId = "drstrange"},
                /* bugtracker ticket */ new Ticket { Id = 2, Title = "Ticket History Helper", Description = "Write helper to help assign properties to ticket history", Created = DateTimeOffset.Now, TicketTypeId = 3, ProjectId = 1, TicketPriorityId = 3, TicketStatusId = 2, OwnerUserId = "starlord", AssignedToUserId = "antman" },
                /* bugtracker ticket */ new Ticket { Id = 3, Title = "Details Modal Design", Description = "Finish designing ticket details modal feature", Created = DateTimeOffset.Now, TicketTypeId = 2, ProjectId = 1, TicketPriorityId = 3, TicketStatusId = 2, OwnerUserId = "starlord", AssignedToUserId = "drstrange" },
                /* bugtracker ticket */ new Ticket { Id = 4, Title = "Abstract to Helpers", Description = "Abstract access methods to their own helper class", Created = DateTimeOffset.Now, TicketTypeId = 3, ProjectId = 1, TicketPriorityId = 2, TicketStatusId = 2, OwnerUserId = "spiderman", AssignedToUserId = "antman" },
                
                /* blog ticket */ new Ticket { Id = 6, Title = "Impliment Kit", Description = "Add Material UI kit to site instead of current template", Created = DateTimeOffset.Now, TicketTypeId = 2, ProjectId = 3, TicketPriorityId = 2, TicketStatusId = 2, OwnerUserId = "warmachine", AssignedToUserId = "thehulk" },
                /* blog ticket */ new Ticket { Id = 7, Title = "Scroll Bar", Description = "Add custom scroll bar to blog", Created = DateTimeOffset.Now, TicketTypeId = 8, ProjectId = 3, TicketPriorityId = 1, TicketStatusId = 2, OwnerUserId = "scarletwitch", AssignedToUserId = "thehulk" },
                
                /* mythos ticket */ new Ticket { Id = 8, Title = "Schema Design", Description = "Lay out database structure and modeling", Created = DateTimeOffset.Now, TicketTypeId = 6, ProjectId = 4, TicketPriorityId = 1, TicketStatusId = 2, OwnerUserId = "mantis", AssignedToUserId = "rocket" },
                /* mythos ticket */ new Ticket { Id = 9, Title = "Layout", Description = "Build Layout UI for site", Created = DateTimeOffset.Now, TicketTypeId = 2, ProjectId = 4, TicketPriorityId = 1, TicketStatusId = 2, OwnerUserId = "mantis", AssignedToUserId = "rocket" },

                /* portfolio ticket */ new Ticket { Id = 10, Title = "Blackjack Cards", Description = "Add cards to blackjack script game", Created = DateTimeOffset.Now, TicketTypeId = 2, ProjectId = 2, TicketPriorityId = 2, TicketStatusId = 2, OwnerUserId = "falcon", AssignedToUserId = "vision" },
                /* portfolio ticket */ new Ticket { Id = 11, Title = "About Me Section", Description = "Impliment blockquote for text, change image to rectangle instead of circle", Created = DateTimeOffset.Now, TicketTypeId = 2, ProjectId = 2, TicketPriorityId = 1, TicketStatusId = 2, OwnerUserId = "pepperpotts", AssignedToUserId = "shuri" },
                /* portfolio ticket */ new Ticket { Id = 12, Title = "Particles.js", Description = "Impliment particles framework to header or background", Created = DateTimeOffset.Now, TicketTypeId = 2, ProjectId = 2, TicketPriorityId = 1, TicketStatusId = 2, OwnerUserId = "pepperpotts", AssignedToUserId = "vision" },
                /* portfolio ticket */ new Ticket { Id = 5, Title = "Header Styling", Description = "Add SVG gradient effect to header", Created = DateTimeOffset.Now, TicketTypeId = 2, ProjectId = 2, TicketPriorityId = 1, TicketStatusId = 2, OwnerUserId = "falcon", AssignedToUserId = "shuri" }
                );
            context.SaveChanges();
            #endregion


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}