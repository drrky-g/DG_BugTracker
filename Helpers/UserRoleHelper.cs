using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Helpers
{
    public class UserRoleHelper
    {
        //creates instance of UserManager and provides context for it
        private UserManager<ApplicationUser> userManager = new
        UserManager<ApplicationUser>(new UserStore<ApplicationUser>
        (new ApplicationDbContext()));

        private ApplicationDbContext db = new ApplicationDbContext();

        //Checks to see if a user is in a specified role
        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        //Returns a list of roles for a specific user
        public ICollection<string> ListUserRoles(string userId)
        {
            
            return userManager.GetRoles(userId);
        }

        //Assigns a user to a role
        public bool AddUserToRole(string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        //Removes a user from a role
        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = userManager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }

        //Creates a list of all users in a specific role
        public ICollection<ApplicationUser> UsersInRole (string roleName)
        {
           //sets results list to be a list of entire user profiles
            var resultList = new List<ApplicationUser>();
            //creates a list of all users
            var List = userManager.Users.ToList();
            //populates list with user profiles if they are in the role you're searching
            foreach (var user in List)
            {
                if (IsUserInRole(user.Id, roleName))
                    resultList.Add(user);
            }
            //return a list of user accounts that have the specified role
            return resultList;
        }

        //Creates a list of all users not in a specific role
        public ICollection<ApplicationUser> UsersNotInRole(string roleName)
        {
            var resultList = new List<ApplicationUser>();
            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (!IsUserInRole(user.Id, roleName))
                    resultList.Add(user);
            }

            return resultList;
        }

    }
}