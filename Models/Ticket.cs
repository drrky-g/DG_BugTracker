using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DG_BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The title must be between {2} and {1} characters long", MinimumLength = 5)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        [Display(Name = "Type")]
        public int TicketTypeId { get; set; }
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        [Display(Name = "Priority")]
        public int TicketPriorityId { get; set; }
        [Display(Name = "Status")]
        public int TicketStatusId { get; set; }
        [Display(Name = "Submitter")]
        public string OwnerUserId { get; set; }
        [Display(Name = "Assigned Developer")]
        public string AssignedToUserId { get; set; }

        //-------virtual------------------------------------

        // foreign--------------------------------------------

        public virtual TicketType TicketType { get; set; }
        public virtual Project Project { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }

        //-------------icollections-------------------------------------

        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        //-------------------------constructor--------------------------

        public Ticket()
        {
            this.TicketComments = new HashSet<TicketComment>();
            this.TicketAttachments = new HashSet<TicketAttachment>();
            this.TicketHistories = new HashSet<TicketHistory>();
        }
    }
}