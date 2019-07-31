using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Models
{
    public class TicketType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // virtual ----------------------

        public virtual ICollection<Ticket> Tickets { get; set; }

        // constructor ---------------------

        public TicketType()
        {
            this.Tickets = new HashSet<Ticket>();
        }
    }
}