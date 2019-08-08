using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DG_BugTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotAllowedTicket()
        {
            ViewBag.Message = "You're not assigned to this ticket";

            return View();
        }

        public ActionResult NotAllowedProject()
        {
            ViewBag.Message = "You're not assigned to this project.";

            return View();
        }
    }
}