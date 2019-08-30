using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }
        [Required]
        public string Property { get; set; }
        [Required]
        public string OldValue { get; set; }
        [Required]
        public string NewValue { get; set; }
        [Required]
        public DateTimeOffset Changed { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }

        // virtual -----------------------------------

        // foreign--------------------------------------------

        public virtual ApplicationUser User { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}