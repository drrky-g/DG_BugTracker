using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace DG_BugTracker.Helpers
{
    public class HistoryHelper : InstanceHelper
    {
        
        public static void RecordHistory(Ticket origin, Ticket edit)
        {
            foreach (var property in WebConfigurationManager.AppSettings["ticketproperties"].Split(','))
            {
                var oldVal = origin.GetType().GetProperty(property).GetValue(origin, null).ToString();
                var newVal = edit.GetType().GetProperty(property).GetValue(edit, null).ToString();

                if(oldVal != newVal)
                {
                    var createHistory = new TicketHistory
                    {
                        Property = property,
                        OldValue = oldVal,
                        NewValue = newVal,
                        UserId = HttpContext.Current.User.Identity.GetUserId(),
                        Changed = DateTimeOffset.Now
                        //if its a nullable DateTimeOffset do 'DateTimeOffset.Now.GetValueOrDefault()'
                    };
                    db.TicketHistories.Add(createHistory);
                }
            }
            db.SaveChanges();
        }
        
        
        
        
        
    }
}