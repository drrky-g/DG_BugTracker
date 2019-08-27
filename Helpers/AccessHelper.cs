using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Helpers
{
    public class AccessHelper : InstanceHelper
    {
        //User can see details of a ticket
        public bool CanSeeDetails(Ticket ticket)
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            var meRole = roleHelper.ListUserRoles(me).FirstOrDefault();

            switch (meRole)
            {
                case "Admin":
                    return true;
                case "Project Manager":
                    if (projectHelper.IsOnProject(me, ticket.ProjectId))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "Developer":
                    if (ticket.AssignedToUserId == me)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "Submitter":
                    if(ticket.OwnerUserId == me)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        //can edit title, description, and tickettype
        public bool CanPerformBasicTicketEdit(Ticket ticket)
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            if( me == ticket.OwnerUserId || me == ticket.AssignedToUserId)
            {
                return true;
            }
            else if(projectHelper.IsOnProject(me, ticket.ProjectId) && userManager.IsInRole(me, "Project Manager"))
            {
                return true;
            }
            else if(roleHelper.ListUserRoles(me).FirstOrDefault() == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //can assign users to roles
        public bool CanAssignUserRoles()
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            if (roleHelper.ListUserRoles(me).FirstOrDefault() == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //can archive a project
        public bool CanArchiveProjects(Project project)
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            if(roleHelper.ListUserRoles(me).FirstOrDefault() == "Admin")
            {
                return true;
            }
            else if(roleHelper.ListUserRoles(me).FirstOrDefault() == "Project Manager" && projectHelper.IsOnProject(me, project.Id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Can edit a projects name and details
        public bool CanPerformProjectEdit( Project project)
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            if(roleHelper.ListUserRoles(me).FirstOrDefault() == "Admin")
            {
                return true;
            }
            else if(roleHelper.ListUserRoles(me).FirstOrDefault() == "Project Manager" && projectHelper.IsOnProject(me, project.Id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //can edit a tickets status or priority
        public bool CanEditStatusOrPriority( Ticket ticket)
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            if (roleHelper.ListUserRoles(me).FirstOrDefault() == "Admin")
            {
                return true;
            }
            else if(roleHelper.ListUserRoles(me).FirstOrDefault()== "Project Manager" && projectHelper.IsOnProject(me, ticket.ProjectId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //retrieves a list of tickets the user has access to
        public List<Ticket> GetMyTickets()
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            var myRole = roleHelper.ListUserRoles(me).FirstOrDefault();

            var myTickets = new List<Ticket>();

            switch (myRole)
            {
                case "Admin":
                    myTickets = db.Tickets.ToList();
                    break;
                case "Project Manager":
                    myTickets = db.Users.Find(me).Projects.SelectMany(project => project.Tickets).ToList();
                    break;
                case "Developer":
                    myTickets = db.Tickets.AsNoTracking().Where(tkt => tkt.AssignedToUserId == me).ToList();
                    break;
                case "Submitter":
                    myTickets = db.Tickets.AsNoTracking().Where(tkt => tkt.OwnerUserId == me).ToList();
                    break;
            }
            return myTickets;
        }

        
    }
}