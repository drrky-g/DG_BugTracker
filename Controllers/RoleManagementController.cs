using DG_BugTracker.Helpers;
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
        private UserRoleHelper roleHelper = new UserRoleHelper();

        
        //
        // GET: RoleManagement
        public ActionResult UserIndex()
        {
            var allusers = db.Users.Select(user => new UserProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarPath = user.AvatarPath,
                Email = user.Email
            }).ToList();

            return View(allusers);
        }

        //
        //GET: ManageSingleRole
        public ActionResult ManageSingleRole(string userId)
        {
            //SYNTAX FOR SelectList()
            // SelectList(
            //   1:  list of data pushed into control,
            //   2:  communicate column selection to Post, 
            //   3:  column we show user inside control,
            //   4:  if role is already occupied show this)


            var currentRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            ViewBag.UserId = userId;
            ViewBag.UserRole = new SelectList(db.Roles.ToList(), "Name", "Name", currentRole);
            ViewBag.UserName = db.Users.Find(userId).FullName;
            return View();
            
        }

        //
        //POST: ManageSingleRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageSingleRole(string userId, string userRole)
        {
            //remove user from any role they have assigned already
            foreach (var role in roleHelper.ListUserRoles(userId))
            {
                roleHelper.RemoveUserFromRole(userId, role);
            }
            //if userRole is not null, assign them the role selected
            if (!string.IsNullOrEmpty(userRole))
            {
                roleHelper.AddUserToRole(userId, userRole);
            }

            return RedirectToAction("UserIndex");
        }

        //
        //GET: ManageMultipleRoles
        public ActionResult ManageMultipleRoles()
        {

            ViewBag.Users = new MultiSelectList(db.Users.ToList(), "Id", "FullNamePlusEmail");
            ViewBag.RoleName = new SelectList(db.Roles.ToList(), "Name", "Name");

            var allusers = db.Users.Select(user => new UserProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarPath = user.AvatarPath,
                Email = user.Email
            }).ToList();

            return View(allusers);
            //dont forget to add view! (right click)
        }

        //
        //POST: ManageMultipleRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageMultipleRoles(List<string> users, string roleName)
        {//--------------------------------------^^^^^^^^^^^^^^^^^communicates entire list of users to action


            //check to make sure users are actually selected
            if(users != null)
            {
                //Iterate over list of selected users from MultiSelectList
                foreach (var userId in users)
                {
                    //remove user from any occupied role
                    foreach (var role in roleHelper.ListUserRoles(userId))
                    {
                        roleHelper.RemoveUserFromRole(userId, role);
                    }
                    //add user back to selected role in SelectList
                    roleHelper.AddUserToRole(userId, roleName);

                    if (!string.IsNullOrEmpty(roleName))
                    {
                        roleHelper.AddUserToRole(userId, roleName);
                    }
                }
            }
            return RedirectToAction("ManageMultipleRoles");
        }

        //
        //GET: ManageProject
        public ActionResult ManageProject(string id)
        {
            return View(id);
        }

        //
        //POST: ManageProject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProject(UserProfileViewModel id)
        {
            return View(id);
        }
    }
}