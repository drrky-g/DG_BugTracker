using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }

        // virtual-----------------------------

        public virtual ApplicationUser User { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}