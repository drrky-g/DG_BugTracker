using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DG_BugTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string RecieverId { get; set; }
        public string SenderId { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string NotificationBody { get; set; }
        public bool ReadStatus { get; set; }


        // virtual -------------------------------

        //foreign -------------------------------

        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser Reciever { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }
}