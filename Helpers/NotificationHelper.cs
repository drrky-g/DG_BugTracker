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
                GenerateAssignmentNotification(oldTicket, newTicket);
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
            HistoryHelper.RecordDeveloperAssignment(oldTicket, newTicket);
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
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

            HistoryHelper.RecordDeveloperAssignment(oldTicket, newTicket);
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
        
        //my version of creating edit notifications
        public static void CreateEditNotification(Ticket origin, Ticket edit)
        {
            var properties = new Stack<string>();
            var oldValues = new Stack<string>();
            var newValues = new Stack<string>();
            var ticker = new List<int>();

            if(origin.Title != edit.Title)
            {
                properties.Push("Title");
                oldValues.Push(origin.Title);
                newValues.Push(edit.Title);
                ticker.Add(1);
            }
            if (origin.Description != edit.Description)
            {
                properties.Push("Description");
                oldValues.Push(origin.Description);
                newValues.Push(edit.Description);
                ticker.Add(1);
            }
            if (origin.TicketTypeId != edit.TicketTypeId)
            {
                properties.Push("Type");
                oldValues.Push(origin.TicketType.Name);
                newValues.Push(edit.TicketType.Name);
                ticker.Add(1);
            }
            if (origin.TicketPriorityId != edit.TicketPriorityId)
            {
                properties.Push("Priority");
                oldValues.Push(origin.TicketPriority.Name);
                newValues.Push(edit.TicketPriority.Name);
                ticker.Add(1);
            }
            if (origin.TicketStatusId != edit.TicketStatusId)
            {
                properties.Push("Status");
                oldValues.Push(origin.TicketStatus.Name);
                newValues.Push(edit.TicketStatus.Name);
                ticker.Add(1);
            }

            //now, for each edit.. we will create a new string and add it to a stringbuilder object
            var body = new StringBuilder();

            foreach(var item in ticker)
            {
                var prop = properties.Pop();
                var old = oldValues.Pop();
                var nu = newValues.Pop();

                //this creates history entries each time this loops
                HistoryHelper.AddHistory(prop, old, nu, edit.Id);

                //for each change, we make a section and add it to the StringBuilder body instance
                body.AppendLine("");//line break
                body.AppendLine($"{prop} changed from {old} to {nu}.");//property change summary
                body.AppendLine("");//line break

                
            }
            //saves history entries to db
            db.SaveChanges();
            //clear ticker counter when loop is over
            ticker.Clear();

            if (!string.IsNullOrEmpty(body.ToString())){

                //combine header with body
                var entry = new StringBuilder();
                entry.AppendLine($"Ticket {edit.Id} was modified ({DateTime.Now})");
                entry.AppendLine("");
                entry.AppendLine(body.ToString());

                //create notification
                var notification = new TicketNotification
                {
                    TicketId = edit.Id,
                    Created = DateTime.Now,
                    RecieverId = edit.AssignedToUserId,
                    SenderId = HttpContext.Current.User.Identity.GetUserId(),
                    NotificationBody = entry.ToString(),
                    ReadStatus = false
                };
                db.TicketNotifications.Add(notification);
                db.SaveChanges();
            }
        }

        //list of notifications for user
        public static List<TicketNotification> MyUnreadNotifications()
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            return db.TicketNotifications.Where(notification => notification.RecieverId == me && !notification.ReadStatus).ToList();
        }
        //unread notification count
        public static int UnreadNotificationCount()
        {
            var me = HttpContext.Current.User.Identity.GetUserId();
            var myCount = db.TicketNotifications.Where(n => n.RecieverId == me && !n.ReadStatus).Count();
            return myCount;
        }
        //list of read notifications
        public static List<TicketNotification> MyReadNotifications()
        {
            var me = HttpContext.Current.User.Identity.GetUserId();

            return db.TicketNotifications.Where(notification => notification.RecieverId == me && notification.ReadStatus).ToList();
        }
        //read notifications count
        public static int ReadNotificationsCount()
        {
            var me = HttpContext.Current.User.Identity.GetUserId();
            var myCount = db.TicketNotifications.Where(notification => notification.RecieverId == me && notification.ReadStatus).Count();

            return myCount;
        }
    }
}