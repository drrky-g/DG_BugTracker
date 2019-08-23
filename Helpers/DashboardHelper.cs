using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DG_BugTracker.Helpers;
using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace DG_BugTracker.Helpers
{
    public  class DashboardHelper : InstanceHelper
    {
        //instances
        private AccessHelper access = new AccessHelper();

        //Get My Projects
        public ICollection<Project> MyProjectsList()
        {
            var me = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(me);
            return user.Projects.ToList();
        }

        //My Project Count
        public int MyProjectCount()
        {
            return MyProjectsList().Count();
        }

        //Get My Tickets
        public ICollection<Ticket> MyTicketList()
        {
            return access.GetMyTickets();
        }

        //My Ticket Count
        public int MyTicketCount()
        {
            return MyTicketList().Count();
        }


        //Ticket Counts by Labels
        #region Priority Counts

        public int MyLowPriorityCount()
        {
           return MyTicketList().Where(tkt => tkt.TicketPriority.Name == "Low").Count();
        }

        public int MyMediumPriorityCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketPriority.Name == "Medium").Count();
        }

        public int MyHighPriorityCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketPriority.Name == "High").Count();
        }

        public int MyUrgentPriorityCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketPriority.Name == "High").Count();
        }

        #endregion

        #region StatusCounts
        public int MyNewStatusCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketStatus.Name == "New").Count();
        }
        public int MyAssignedStatusCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketStatus.Name == "Assigned").Count();
        }

        public int MyInProgressStatusCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketStatus.Name == "In Progress").Count();
        }

        public int MyOnHoldStatusCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketStatus.Name == "On Hold").Count();
        }

        public int MyReviewStatusCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketStatus.Name == "Ready For Review").Count();
        }
        #endregion

        #region Type Counts
        public int MyBugTypeCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketType.Name == "Bug/Defect").Count();
        }
        public int MyFETypeCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketType.Name == "CSS/Javascript Issue").Count();
        }

        public int MyControllerTypeCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketType.Name == "Controller Issue").Count();
        }

        public int MyViewTypeCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketType.Name == "View Issue").Count();
        }

        public int MyVMTypeCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketType.Name == "ViewModel Issue").Count();
        }

        public int MyModelTypeCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketType.Name == "Model Issue").Count();
        }

        public int MyDocReqTypeCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketType.Name == "Documentation Request").Count();
        }

        public int MyFeatureReqTypeCount()
        {
            return MyTicketList().Where(tkt => tkt.TicketType.Name == "Feature Request").Count();
        }
        #endregion

        //5 Most Recents...

        //Ticket Comments
        public ICollection<TicketComment> FiveRecentComments()
        {
            var allComments = db.TicketComments.ToList();
            var commentList = new List<TicketComment>();
            var myTickets = MyTicketList();
            foreach (var comment in allComments)
            {
                var ticketId = comment.TicketId;
                foreach(var ticket in myTickets)
                {
                    if(ticket.Id == ticketId)
                    {
                        commentList.Add(comment);
                    }
                }
            }
            return commentList.OrderByDescending(comment => comment.Created).Take(5).ToList();
        }

        public ICollection<TicketAttachment> FiveRecentAttachments()
        {
            var allAttachments = db.TicketAttachments.ToList();
            var attachmentList = new List<TicketAttachment>();
            var myTickets = MyTicketList();
            foreach(var attachment in allAttachments)
            {
                var ticketId = attachment.TicketId;
                foreach(var ticket in myTickets)
                {
                    if(ticket.Id == ticketId)
                    {
                        attachmentList.Add(attachment);
                    }
                }
            }
            return attachmentList.OrderByDescending(attach => attach.Created).Take(5).ToList();
        }

        public ICollection<TicketNotification> FiveRecentNotifications()
        {
            var me = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Where(notif => notif.RecieverId == me).OrderByDescending(notif => notif.Created).Take(5).ToList(); 
        }
    }
}