using DG_BugTracker.Helpers;
using DG_BugTracker.Models;
using DG_BugTracker.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DG_BugTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext dB = new ApplicationDbContext();
        private ProjectHelper pH = new ProjectHelper();
        private AccessHelper access = new AccessHelper();
        private DashboardHelper dashboard = new DashboardHelper();

        public ActionResult AccessError()
        {
            ViewBag.Header = "Access Error";


            return View();
        }
        public ActionResult NotAllowedTicket()
        {
            ViewBag.Header = "Ticket Access Error";
            ViewBag.Message = "You're not assigned to this ticket.";

            return View("AccessError");
        }

        public ActionResult NotAllowedProject()
        {
            ViewBag.Header = "Project Access Error";
            ViewBag.Message = "You're not assigned to this project.";

            return View("AccessError");
        }

        public ActionResult ChangesNotSaved()
        {
            ViewBag.Header = "Profile Update Error";
            ViewBag.Message = "There was an error updating your profile";

            return View("AccessError");
        }

        public ActionResult DeveloperOnly()
        {
            ViewBag.Header = "Error";
            ViewBag.Subheader = "Only this ticket's developer can do that.";
            return View("AccessError");
        }

        public ActionResult Dashboard()
        {
            var myDashboard = new MyDashboard
            {
                MyTickets = dashboard.MyTicketList(),
                MyProjects = dashboard.MyProjectsList(),
                LowCount = dashboard.MyLowPriorityCount(),
                MediumCount = dashboard.MyMediumPriorityCount(),
                HighCount = dashboard.MyHighPriorityCount(),
                UrgentCount = dashboard.MyUrgentPriorityCount(),
                NewCount = dashboard.MyNewStatusCount(),
                AssignedCount = dashboard.MyAssignedStatusCount(),
                InProgressCount = dashboard.MyInProgressStatusCount(),
                OnHoldCount = dashboard.MyOnHoldStatusCount(),
                ReadyForReviewCount = dashboard.MyReviewStatusCount(),
                BugCount = dashboard.MyBugTypeCount(),
                FECount = dashboard.MyFETypeCount(),
                ControllerCount = dashboard.MyControllerTypeCount(),
                ViewCount = dashboard.MyViewTypeCount(),
                VMCount = dashboard.MyVMTypeCount(),
                ModelCount = dashboard.MyModelTypeCount(),
                DocReqCount = dashboard.MyDocReqTypeCount(),
                FeatureReqCount = dashboard.MyFeatureReqTypeCount(),
                MyTicketCount = dashboard.MyTicketCount(),
                MyProjectCount = dashboard.MyProjectCount(),
                RecentComments = dashboard.FiveRecentComments(),
                RecentAttachments = dashboard.FiveRecentAttachments(),
                RecentNotifications = dashboard.FiveRecentNotifications(),
                UserName = dB.Users.Find(User.Identity.GetUserId()).FirstName
            };

            ViewBag.TicketPriorityDropDown = new SelectList(dB.TicketPriorities, "Id", "Name");
            ViewBag.TicketTypeDropDown = new SelectList(dB.TicketTypes, "Id", "Name");

            return View(myDashboard);
        }

        
    }
}