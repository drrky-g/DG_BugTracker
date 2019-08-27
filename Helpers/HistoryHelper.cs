using System;
using System.Collections.Generic;
using System.Web;
using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace DG_BugTracker.Helpers
{
    public class HistoryHelper : InstanceHelper
    {
        public static void AddHistory(string property, string oldValue, string newValue, int ticketId)
        {
            var nuHistory = new TicketHistory
            {
                Property = property,
                OldValue = oldValue,
                NewValue = newValue,
                Changed = DateTimeOffset.Now,
                UserId = HttpContext.Current.User.Identity.GetUserId(),
                TicketId = ticketId
            };
            db.TicketHistories.Add(nuHistory);
        }

        public static string OldDeveloperWord(Ticket origin)
        {
            var oldDev = origin.AssignedToUserId;

            if (string.IsNullOrEmpty(oldDev))
            {
                return "Unassigned";
            }
            else
            {
                return origin.AssignedToUser.FullName;
            }
        }

        public static void RecordDeveloperAssignment(Ticket origin, Ticket edit)
        {
            var ticketId = origin.Id;
            var history = new TicketHistory
            {
                Property = "Developer Assignment",
                OldValue = OldDeveloperWord(origin),
                NewValue = edit.AssignedToUser.FullName,
                Changed = DateTime.Now,
                UserId = HttpContext.Current.User.Identity.GetUserId(),
                TicketId = ticketId
            };
            db.TicketHistories.Add(history);
            db.SaveChanges();
        }



    }
}