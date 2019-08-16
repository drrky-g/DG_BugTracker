using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace DG_BugTracker.Helpers
{
    
    public class NotificationHelper : InstanceHelper
    {

        
        public static void ManageNotifications(Ticket origin, Ticket edit)
        {
            CreateAssignmentNotification(origin, edit);
            CreateEditNotification(origin, edit);
        }

        //determines which notification to send
        public static void CreateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var noChange = (oldTicket.AssignedToUserId == newTicket.AssignedToUserId);
            var assignment = (string.IsNullOrEmpty(oldTicket.AssignedToUserId));
            var unassignment = (string.IsNullOrEmpty(newTicket.AssignedToUserId));

            if (noChange)
            {
                return;
            }

            if (assignment)
            {
                GenerateUnassignmentNotification(oldTicket, newTicket);
            } else if (unassignment)
            {
                GenerateUnassignmentNotification(oldTicket, newTicket);
            }
            else
            {
                GenerateAssignmentNotification(oldTicket, newTicket);
                GenerateUnassignmentNotification(oldTicket, newTicket);
            }
        }

        //Unassignment Notification
        public static void GenerateUnassignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                //Subject = $"You were unassigned from Ticket {newTicket.Id} on {DateTime.Now.ToString("MM/dd/yyyy h:mm tt")}",
                ReadStatus = false,
                RecieverId = oldTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBody = $"You were unassigned from Ticket {newTicket.Id} on {DateTime.Now.ToString("MM/dd/yyyy h:mm tt")}.",
                TicketId = newTicket.Id
            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

        //Assignment Notification
        public static void GenerateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                //Subject = $"You were unassigned from Ticket {newTicket.Id} on {DateTime.Now.ToString("MM/dd/yyyy h:mm tt")}",
                ReadStatus = false,
                RecieverId = newTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBody = $"You were assigned to Ticket {oldTicket.Id} on {DateTime.Now.ToString("MM/dd/yyyy h:mm tt")}.",
                TicketId = oldTicket.Id
            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

        //list of notifications for user
        public static List<TicketNotification> MyUnreadNotifications()
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            return db.TicketNotifications.Where(notification => notification.RecieverId == me && !notification.ReadStatus).ToList();
        }

        private static void CreateEditNotification(Ticket origin, Ticket edit)
        {
            var entry = new StringBuilder();

            foreach (var property in WebConfigurationManager.AppSettings["ticketproperties"].Split(','))
            {
                var old = origin.GetType().GetProperty(property).GetValue(origin, null);
                var nu = edit.GetType().GetProperty(property).GetValue(edit, null);

                if (old != nu)
                {
                    entry.AppendLine
                }
            }
        }
    }
}