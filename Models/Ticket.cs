using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DG_BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; } //targetted
        [Display(Name = "Name")]
        //[StringLength(50, ErrorMessage = "The title must be between {2} and {1} characters long", MinimumLength = 5)]
        public string Title { get; set; } //targetted
        public string Description { get; set; } //targetted
        public DateTimeOffset Created { get; set; } //targetted
        public DateTimeOffset? Updated { get; set; } //targetted
        [Display(Name = "Type")]
        public int TicketTypeId { get; set; } //targetted
        [Display(Name = "Project")]
        public int ProjectId { get; set; } //targetted
        [Display(Name = "Priority")]
        public int TicketPriorityId { get; set; } //targetted
        [Display(Name = "Status")]
        public int TicketStatusId { get; set; } //targetted
        [Display(Name = "Submitter")]
        public string OwnerUserId { get; set; } //targetted
        [Display(Name = "Assigned Developer")]
        public string AssignedToUserId { get; set; } //targetted


        //-------virtual------------------------------------

        // foreign--------------------------------------------

        public virtual TicketType TicketType { get; set; }
        public virtual Project Project { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }
        //---------------- id targetting strings ------------------------------------
        [NotMapped]
        public string RowTarget
        {
            get
            {
                return $"t{Id}row";
            }
        }
        public string BtnTarget
        {
            get
            {
                return $"t{Id}btn";
            }
        }
        [NotMapped]
        public string IdTarget
        {
            get
            {
                return $"t{Id}Id";
            }
        }
        [NotMapped]
        public string TitleTarget
        {
            get
            {
                return $"t{Id}Title";
            }
        }
        [NotMapped]
        public string DescriptionTarget
        {
            get
            {
                return $"t{Id}Desc";
            }
        }
        [NotMapped]
        public string CreatedTarget
        {
            get
            {
                return $"t{Id}Created";
            }
        }
        [NotMapped]
        public string UpdatedTarget
        {
            get
            {
                return $"t{Id}Updated";
            }
        }
        [NotMapped]
        public string ProjectTarget
        {
            get
            {
                return $"t{Id}Project";
            }
        }
        [NotMapped]
        public string TypeTarget
        {
            get
            {
                return $"t{Id}Type";
            }
        }
        [NotMapped]
        public string StatusTarget
        {
            get
            {
                return $"t{Id}Status";
            }
        }
        [NotMapped]
        public string PriorityTarget
        {
            get
            {
                return $"t{Id}Priority";
            }
        }
        [NotMapped]
        public string SubTarget
        {
            get
            {
                return $"t{Id}Sub";
            }
        }
        [NotMapped]
        public string DevTarget
        {
            get
            {
                return $"t{Id}Dev";
            }
        }
        [NotMapped]
        public string CommentContainerTarget
        {
            get
            {
                return $"t{Id}Comms";
            }
        }
        [NotMapped]
        public string AttachmentContainerTarget
        {
            get
            {
                return $"t{Id}Attach";
            }
        }
        [NotMapped]
        public string HistoryContainerTarget
        {
            get
            {
                return $"t{Id}Hist";
            }
        }
        [NotMapped]
        public string NotifContainerTarget
        {
            get
            {
                return $"t{Id}Notif";
            }
        }


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