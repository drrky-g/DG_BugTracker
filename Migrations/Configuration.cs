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

            //users on DB
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
                    DisplayName = "Fury",
                    AvatarPath = "/Avatars/fury.jpeg"
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
                    DisplayName = "Iron Man",
                    AvatarPath = "/Avatars/ironman.jpg"
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
                    DisplayName = "Captain America",
                    AvatarPath = "/Avatars/cap.jpg"
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
                    DisplayName = "Black Widow",
                    AvatarPath = "Avatars/nat.jpg"
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
                    DisplayName = "Captain Marvel",
                    AvatarPath = "/Avatars/danvers.jpg"
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
                    DisplayName = "Thor",
                    AvatarPath = "/Avatars/thor.jpg"
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
                    DisplayName = "Black Panther",
                    AvatarPath = "/Avatars/tchalla.jpg"
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
                    DisplayName = "Hulk",
                    AvatarPath = "/Avatars/hulk.jpg"
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
                    DisplayName = "Vision",
                    AvatarPath = "/Avatars/vision.jpg"
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
                    DisplayName = "AntMan",
                    AvatarPath = "/Avatars/antman.jpg"
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
                    DisplayName = "Dr. Strange",
                    AvatarPath = "/Avatars/strange.jpg"
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
                    DisplayName = "Rocket",
                    AvatarPath = "/Avatars/rocket.jpg"
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
                    DisplayName = "Shuri",
                    AvatarPath = "/Avatars/shuri.png"
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
                    DisplayName = "Hawkeye",
                    AvatarPath = "/Avatars/hawkeye.jpg"
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
                    DisplayName = "Spider Man",
                    AvatarPath = "/Avatars/spiderman.png"
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
                    DisplayName = "Star Lord",
                    AvatarPath = "/Avatars/starlord.jpg"
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
                    DisplayName = "War Machine",
                    AvatarPath = "/Avatars/warmachine.jpg"
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
                    DisplayName = "Mantis",
                    AvatarPath = "/Avatars/mantis.png"
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
                    DisplayName = "Scarlet Witch",
                    AvatarPath = "/Avatars/scarletwitch.jpg"
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
                    DisplayName = "Winter Soldier",
                    AvatarPath = "/Avatars/wintersoldier.jpg"
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
                    DisplayName = "Pepper",
                    AvatarPath = "/Avatars/pepperpotts.jpg"
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
                    DisplayName = "Falcon",
                    AvatarPath = "/Avatars/falcon.jpg"
                }, avengerPassword);
            }
            context.SaveChanges();
            #endregion

            //role assignments on DB
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

            //on DB
            //Seed the models below, if you want to wipe and restart your database//
            #region Seeded Projects
            context.Projects.AddOrUpdate(
                project => project.Name,
                new Project { Name = "Bug Tracker", Description = "Debugging application to track and manage a software teams debugging efforts.", Created = DateTime.Now },
                new Project { Name = "Drrk.io", Description = "Portfolio website of Derrick Gordon, an ASP .NET Developer.", Created = DateTime.Now },
                new Project { Name = "Blog Website", Description = "A personal blog for Derrick Gordon.", Created = DateTime.Now },
                new Project { Name = "Mythos Website", Description = "An educational website about mythology.", Created = DateTime.Now }
                );
            context.SaveChanges();
            #endregion

            #region Project Assignments
            // assign each id to a variable

            //ProjectIds
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

            ////on DB
            #region  Seeded Ticket Types
            //context.TicketTypes.AddOrUpdate(
            //    type => type.Name,
            //    new TicketType { Name = "Bug/Defect" },
            //    new TicketType { Name = "CSS/Javascript Issue" },
            //    new TicketType { Name = "Controller Issue" },
            //    new TicketType { Name = "View Issue" },
            //    new TicketType { Name = "ViewModel Issue" },
            //    new TicketType { Name = "Model Issue" },
            //    new TicketType { Name = "Documentation Request" },
            //    new TicketType { Name = "Feature Request" }
            //    );



            //context.SaveChanges();
            //#endregion
            ////on DB
            //#region Seeded Ticket Priorities
            //context.TicketPriorities.AddOrUpdate(
            //    prior => prior.Name,
            //    new TicketPriority { Name = "Low" },
            //    new TicketPriority { Name = "Medium" },
            //    new TicketPriority { Name = "High" },
            //    new TicketPriority { Name = "Urgent" }
            //    );



            //context.SaveChanges();
            //#endregion
            ////on DB
            //#region Seeded Ticket Statuses
            //context.TicketStatuses.AddOrUpdate(
            //    status => status.Name,
            //    new TicketStatus { Name = "New" },
            //    new TicketStatus { Name = "Assigned" },
            //    new TicketStatus { Name = "In Progress" },
            //    new TicketStatus { Name = "On Hold" },
            //    new TicketStatus { Name = "Ready for Review" },
            //    new TicketStatus { Name = "Archived" }
            //    );




            //context.SaveChanges();
            #endregion


            #region Seeded Tickets 

            //foreign key variables
            #region Variables for foreign keys
            //Types
            var bug = context.TicketTypes.FirstOrDefault(type => type.Name == "Bug/Defect").Id;
            var style = context.TicketTypes.FirstOrDefault(type => type.Name == "CSS/Javascript Issue").Id;
            var controller = context.TicketTypes.FirstOrDefault(type => type.Name == "Controller Issue").Id;
            var view = context.TicketTypes.FirstOrDefault(type => type.Name == "View Issue").Id;
            var viewmodel = context.TicketTypes.FirstOrDefault(type => type.Name == "ViewModel Issue").Id;
            var model = context.TicketTypes.FirstOrDefault(type => type.Name == "Model Issue").Id;
            var docuRequest = context.TicketTypes.FirstOrDefault(type => type.Name == "Documentation Request");
            var featureReq = context.TicketTypes.FirstOrDefault(type => type.Name == "Feature Request").Id;

            //Priorities
            var lowPriority = context.TicketPriorities.FirstOrDefault(prior => prior.Name == "Low").Id;
            var medPriority = context.TicketPriorities.FirstOrDefault(prior => prior.Name == "Medium").Id;
            var highPriority = context.TicketPriorities.FirstOrDefault(prior => prior.Name == "High").Id;
            var urgentPriority = context.TicketPriorities.FirstOrDefault(prior => prior.Name == "Urgent").Id;

            //Statuses
            var nu = context.TicketStatuses.FirstOrDefault(status => status.Name == "New").Id;
            var assigned = context.TicketStatuses.FirstOrDefault(status => status.Name == "Assigned").Id;
            var inprog = context.TicketStatuses.FirstOrDefault(status => status.Name == "In Progress").Id;
            var hold = context.TicketStatuses.FirstOrDefault(status => status.Name == "On Hold").Id;
            var review = context.TicketStatuses.FirstOrDefault(status => status.Name == "Ready for Review").Id;
            var archived = context.TicketStatuses.FirstOrDefault(status => status.Name == "Archived").Id;
            #endregion
            context.Tickets.AddOrUpdate(
                ticket => ticket.Title,
                /* bugtracker ticket */ new Ticket {  Title = "Notification Dropdown", Description = "Set CSS for Notification drop down styling", Created = DateTime.Now, TicketTypeId = style, ProjectId = trackerId, TicketPriorityId = medPriority, TicketStatusId = assigned, OwnerUserId = "spiderman", AssignedToUserId = "drstrange", Updated = DateTime.Now},
                /* bugtracker ticket */ new Ticket {  Title = "Ticket History Helper", Description = "Write helper to help assign properties to ticket history", Created = DateTime.Now, TicketTypeId = style, ProjectId = trackerId, TicketPriorityId = highPriority, TicketStatusId = assigned, OwnerUserId = "starlord", AssignedToUserId = "antman", Updated = DateTime.Now },
                /* bugtracker ticket */ new Ticket {  Title = "Details Modal Design", Description = "Finish designing ticket details modal feature", Created = DateTime.Now, TicketTypeId = style, ProjectId = trackerId, TicketPriorityId = highPriority, TicketStatusId = assigned, OwnerUserId = "starlord", AssignedToUserId = "drstrange", Updated = DateTime.Now },
                /* bugtracker ticket */ new Ticket {  Title = "Abstract to Helpers", Description = "Abstract access methods to their own helper class", Created = DateTime.Now, TicketTypeId = controller, ProjectId = trackerId, TicketPriorityId = medPriority, TicketStatusId = assigned, OwnerUserId = "spiderman", AssignedToUserId = "antman", Updated = DateTime.Now },
                
                /* blog ticket */ new Ticket {  Title = "Impliment Kit", Description = "Add Material UI kit to site instead of current template", Created = DateTime.Now, TicketTypeId = style, ProjectId = blogId, TicketPriorityId = medPriority, TicketStatusId = assigned, OwnerUserId = "warmachine", AssignedToUserId = "thehulk", Updated = DateTime.Now },
                /* blog ticket */ new Ticket {  Title = "Scroll Bar", Description = "Add custom scroll bar to blog", Created = DateTime.Now, TicketTypeId = featureReq, ProjectId = blogId, TicketPriorityId = lowPriority, TicketStatusId = assigned, OwnerUserId = "scarletwitch", AssignedToUserId = "thehulk", Updated = DateTime.Now },
                
                /* mythos ticket */ new Ticket {  Title = "Schema Design", Description = "Lay out database structure and modeling", Created = DateTime.Now, TicketTypeId = model, ProjectId = mythosId, TicketPriorityId = lowPriority, TicketStatusId = assigned, OwnerUserId = "mantis", AssignedToUserId = "rocket", Updated = DateTime.Now },
                /* mythos ticket */ new Ticket {  Title = "Layout", Description = "Build Layout UI for site", Created = DateTime.Now, TicketTypeId = style, ProjectId = mythosId, TicketPriorityId = lowPriority, TicketStatusId = assigned, OwnerUserId = "mantis", AssignedToUserId = "rocket", Updated = DateTime.Now },

                /* portfolio ticket */ new Ticket {  Title = "Blackjack Cards", Description = "Add cards to blackjack script game", Created = DateTime.Now, TicketTypeId = featureReq, ProjectId = portfolioId, TicketPriorityId = medPriority, TicketStatusId = assigned, OwnerUserId = "falcon", AssignedToUserId = "vision", Updated = DateTime.Now },
                /* portfolio ticket */ new Ticket {  Title = "About Me Section", Description = "Impliment blockquote for text, change image to rectangle instead of circle", Created = DateTime.Now, TicketTypeId = style, ProjectId = portfolioId, TicketPriorityId = lowPriority, TicketStatusId = assigned, OwnerUserId = "pepperpotts", AssignedToUserId = "shuri", Updated = DateTime.Now },
                /* portfolio ticket */ new Ticket {  Title = "Particles.js", Description = "Impliment particles framework to header or background", Created = DateTime.Now, TicketTypeId = style, ProjectId = portfolioId, TicketPriorityId = lowPriority, TicketStatusId = assigned, OwnerUserId = "pepperpotts", AssignedToUserId = "vision", Updated = DateTime.Now },
                /* portfolio ticket */ new Ticket {  Title = "Header Styling", Description = "Add SVG gradient effect to header", Created = DateTime.Now, TicketTypeId = style, ProjectId = portfolioId, TicketPriorityId = lowPriority, TicketStatusId = assigned, OwnerUserId = "falcon", AssignedToUserId = "shuri", Updated = DateTime.Now }
                );
            context.SaveChanges();
            #endregion


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}