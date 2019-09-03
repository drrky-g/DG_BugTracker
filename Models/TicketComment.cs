using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Created { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }
        

        // virtual ------------------------------------

        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}