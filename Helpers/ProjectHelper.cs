using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Helpers
{
    public class ProjectHelper : InstanceHelper
    {
        public List<string> UsersInRoleOnProject(int projectId, string roleName)
        {
            var people = new List<string>();

            foreach(var user in UsersOnProject(projectId).ToList())
            {
                if(roleHelper.IsUserInRole(user.Id, roleName))
                {
                    people.Add(user.Id);
                }
            }

            return people;
        }

        public bool IsUserOnProject(string userId, int projectId)
        {
            var project = db.Projects.Find(projectId);
            var flag = project.Users.Any(user => user.Id == userId);
            return (flag);
        }

        public ICollection<Project> ListUserProjects()
        {
            var myId = HttpContext.Current.User.Identity.GetUserId();
            var me = db.Users.Find(myId);

            var projects = me.Projects.ToList();
            if (HttpContext.Current.User.IsInRole("Admin"))
            {
                projects = db.Projects.ToList();
            }

            return (projects);
        }

        //void return type means its not returning any kind of stored variable
        public void AddUserToProject(string userId, int projectId)
        {
            if(!IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var newUser = db.Users.Find(userId);

                proj.Users.Add(newUser);
                db.SaveChanges();
            }
        }


        public void RemoveUserFromProject(string userId, int projectId)
        {
            if(IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var delUser = db.Users.Find(userId);

                proj.Users.Remove(delUser);
                db.Entry(proj).State = System.Data.Entity.EntityState.Modified; //saves object instance
                db.SaveChanges();
            }
        }

        public ICollection<ApplicationUser>UsersOnProject( int projectId)
        {
            return db.Projects.Find(projectId).Users;
        }

        public ICollection<ApplicationUser> UsersNotOnProject(int projectId)
        {
            return db.Users.Where(user => user.Projects.All(proj => proj.Id != projectId)).ToList();
        }

    }
}