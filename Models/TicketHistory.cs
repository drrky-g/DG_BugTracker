using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTimeOffset Changed { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }

        // virtual -----------------------------------

        // foreign--------------------------------------------

        public virtual ApplicationUser User { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}