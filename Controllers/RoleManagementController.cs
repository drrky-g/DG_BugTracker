using DG_BugTracker.Models;
using DG_BugTracker.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DG_BugTracker.Controllers
{
    public class RoleManagementController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        
        //
        // GET: RoleManagement
        public ActionResult UserIndex()
        {
            var allusers = db.Users.Select(user => new UserProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarPath = user.AvatarPath
            }).ToList();

            return View(allusers);
        }

        //
        //GET: ManageSingleRole
        public ActionResult ManageSingleRole()
        {
            return View();
            //dont forget to add view! (right click)
        }

        //
        //POST: ManageSingleRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageSingleRole(ApplicationUser userId)
        {
            if (ModelState.IsValid)
            {

            }
            return View(userId);
        }

        //
        //GET: ManageMultipleRoles
        public ActionResult ManageMultipleRoles()
        {
            return View();
            //dont forget to add view! (right click)
        }

        //
        //POST: ManageMultipleRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageMultipleRoles(ApplicationUser id)
        {
            if (ModelState.IsValid)
            {

            }
            return View(id);
        }
    }
}