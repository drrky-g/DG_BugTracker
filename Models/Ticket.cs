using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public int TicketTypeId { get; set; }
        public int ProjectId { get; set; }
        public int TicketPriorityId { get; set; }
        public int TicketStatusId { get; set; }
        public string OwnerUserId { get; set; }
        public string AssignedToUserId { get; set; }

        //-------virtual------------------------------------

        // foreign--------------------------------------------

        public virtual TicketType TicketType { get; set; }
        public virtual Project Project { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }

        //-------------icollections-------------------------------------

        public virtual ICollection<TicketComment> Comments { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        //-------------------------constructor--------------------------

        public Ticket()
        {
            this.Comments = new HashSet<TicketComment>();
            this.TicketAttachments = new HashSet<TicketAttachment>();
            this.TicketHistories = new HashSet<TicketHistory>();
        }
    }
}