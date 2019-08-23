using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DG_BugTracker.Models;

namespace DG_BugTracker.ViewModels
{
    public class MyDashboard
    {
        //User Name Display
        public string UserName { get; set; }

        //List of projects
        public ICollection<Project> MyProjects { get; set; }

        //List of tickets
        public ICollection<Ticket> MyTickets { get; set; }

        //Assigned Priority Counts
        #region Priority Counts
        public int LowCount { get; set; }
        public int MediumCount { get; set; }
        public int HighCount { get; set; }
        public int UrgentCount { get; set; }
        #endregion

        //Assigned Status Counts
        #region Status Counts
        public int NewCount { get; set; }
        public int AssignedCount { get; set; }
        public int InProgressCount { get; set; }
        public int OnHoldCount { get; set; }
        public int ReadyForReviewCount { get; set; }
        #endregion

        //Assigned Type Counts
        #region Type Counts
        public int BugCount { get; set; }
        public int FECount { get; set; }
        public int ControllerCount { get; set; }
        public int ViewCount { get; set; }
        public int VMCount { get; set; }
        public int ModelCount { get; set; }
        public int DocReqCount { get; set; }
        public int FeatureReqCount { get; set; }
        #endregion

        //Your Ticket Count
        public int MyTicketCount { get; set; }

        //Your Project Count
        public int MyProjectCount { get; set; }

        //5 Most Recent...
        //--------------------------------//
        #region Recent Collections
        //Ticket Comments
        public ICollection<TicketComment> RecentComments { get; set; }
        //Ticket Attachments
        public ICollection<TicketAttachment> RecentAttachments { get; set; }
        //Notifications
        public ICollection<TicketNotification> RecentNotifications { get; set; }
        #endregion










    }
}