using DG_BugTracker.Helpers;
using DG_BugTracker.Models;
using DG_BugTracker.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DG_BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dB = new ApplicationDbContext();
        private UserRoleHelper rH = new UserRoleHelper();
        private ProjectHelper pH = new ProjectHelper();
        private AccessHelper access = new AccessHelper();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AccessError()
        {
            return View();
        }
        public ActionResult NotAllowedTicket()
        {
            ViewBag.Message = "You're not assigned to this ticket.";

            return View("AccessError");
        }

        public ActionResult NotAllowedProject()
        {
            ViewBag.Message = "You're not assigned to this project.";

            return View("AccessError");
        }

        public ActionResult ChangesNotSaved()
        {
            ViewBag.Message = "There was an error updating your profile";

            return View("AccessError");
        }

        public ActionResult Dashboard()
        {


            var myProjects = pH.ListUserProjects();
            var myProjectCount = myProjects.Count();
            var myTickets = access.GetMyTickets();
            var myTicketCount = myTickets.Count();
            //unfortunately i still to use this to grab my users name for the viewbag
            var user = dB.Users.Find(User.Identity.GetUserId());

            var myAssignments = new MyDashboard
            {
                MyTickets = myTickets,
                MyProjects = myProjects
            };

            //TODO: make it so that 'projects' and 'tickets' will switch to singular if they dont have more than 1
            ViewBag.Header = $"Welcome back, {user.FirstName}.";
            ViewBag.Subheader = $"You're assigned to {myProjectCount} projects and {myTicketCount} tickets";

            return View(myAssignments);
        }

        public ActionResult OldDashboard()
        {
            //create a single page that has your tickets and projects
            //this view will dynamically render details "views" and forms via modal

            var userId = User.Identity.GetUserId();
            var myRole = rH.ListUserRoles(userId).FirstOrDefault();
            var myTickets = new List<Ticket>();
            var myProjects = pH.ListUserProjects();




            //role switch...need to abstract to helper method.
            switch (myRole)
            {
                case "Developer":
                    myTickets = dB.Tickets.Where(ticket => ticket.AssignedToUserId == userId).ToList();
                    break;
                case "Submitter":
                    myTickets = dB.Tickets.Where(ticket => ticket.OwnerUserId == userId).ToList();
                    break;
                case "Project Manager":
                    myTickets = dB.Users.Find(userId).Projects.SelectMany(ticket => ticket.Tickets).ToList();
                    break;
                case "Admin":
                    myProjects = dB.Projects.ToList();
                    myTickets = dB.Tickets.ToList();
                    break;
            }



            var myAssignments = new MyDashboard
            {
                MyTickets = myTickets,
                MyProjects = myProjects
            };

            return View(myAssignments);
        }
    }
}