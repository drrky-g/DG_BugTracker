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



    }
}