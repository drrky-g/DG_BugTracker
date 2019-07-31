using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //------------virtual----------------------------
        //------------icollection------------------------
        public virtual ICollection<Ticket> Tickets { get; set; }
        //---------constructor---------------------------
        public Project()
        {
            this.Tickets = new HashSet<Ticket>();
        }
    }
}