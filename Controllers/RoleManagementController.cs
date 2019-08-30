using DG_BugTracker.Helpers;
using DG_BugTracker.Models;
using DG_BugTracker.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DG_BugTracker.Controllers
{
    [Authorize(Roles = "Admin, Project Manager")]
    public class RoleManagementController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        private ProjectHelper projectHelper = new ProjectHelper();

        //SYNTAX FOR SelectList()
        // SelectList(
        //   1:  list of data pushed into control,
        //   2:  property used in post communication, 
        //   3:  property displayed in control,
        //   4:  default selection (usually indicates some sort of pre-existing relationship.
        //       for example: if a ticket is already assigned to a developer, it would show here)
        //   );                        


        //
        // GET: RoleManagement
        [Authorize(Roles = "Admin, Project Manager")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult ManageSingleRole(string userId)
        {
            
            var currentRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            ViewBag.Header = "Role Assignment";
            ViewBag.Subheader = db.Users.Find(userId).FullName;
            ViewBag.UserRole = new SelectList(db.Roles.ToList(), "Name", "Name", currentRole);
            ViewBag.UserId = userId;
            return View();
            
        }

        //
        //POST: ManageSingleRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
        [Authorize( Roles = "Admin")]
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
        [Authorize (Roles = "Admin")]
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
            return RedirectToAction("UserIndex");
        }


        //
        //GET: ManageUsersMultipleProjects
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult ManageUsersMultipleProjects(string userId)
        {
            var thisUser = db.Users.Find(userId);
            var allProjects = db.Projects.ToList();
            var userProjects = projectHelper.ListSpecificUserProjects(userId).Select(proj => proj.Id);
            var thisUserModel = new ManageMultipleProjectsVM
            {
                Id = userId,
                FullName = thisUser.FullName,
                AvatarPath = thisUser.AvatarPath,
                Email = thisUser.Email,
                Role = roleHelper.ListUserRoles(userId).FirstOrDefault(),
                MyProjects = projectHelper.ListSpecificUserProjects(userId)
            };

            ViewBag.ProjectIds = new MultiSelectList(db.Projects.ToList(), "Id", "Name", userProjects);
            return View(thisUserModel);
        }

        //
        //POST: ManageUsersMultipleProjects
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult ManageUsersMultipleProjects(string userId, List<int> projectIds)
        {
            foreach(var project in projectHelper.ListSpecificUserProjects(userId))
            {
                projectHelper.RemoveUserFromProject(userId, project.Id);
            }
            if (projectIds != null)
            {
                foreach(var projectId in projectIds)
                {
                    projectHelper.AddUserToProject(userId, projectId);
                }
            }

            
            
            return RedirectToAction("UserIndex");
        }

        //
        //POST: ManageProjectUsers
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult ManageProjectUsers(int projectId, List<string> ProjectManagers, List<string> Developers, List<string> Submitters)
        {
            //1: remove all users from project
            foreach (var user in projectHelper.UsersOnProject(projectId).ToList())
            {
                projectHelper.RemoveUserFromProject(user.Id, projectId);
            }

            //2: add back selected PMs
            if(ProjectManagers != null)
            {
                foreach(var projectManagerId in ProjectManagers)
                {
                    projectHelper.AddUserToProject(projectManagerId, projectId);
                }
            }

            //3: add back selected Devs
            if (Developers != null)
            {
                foreach (var developerId in Developers)
                {
                    projectHelper.AddUserToProject(developerId, projectId);
                }
            }

            //4: add back selected Submitters
            if (Submitters != null)
            {
                foreach (var submitterId in Submitters)
                {
                    projectHelper.AddUserToProject(submitterId, projectId);
                }
            }

            return RedirectToAction("Dashboard", "Home", null);
        }
    }
}