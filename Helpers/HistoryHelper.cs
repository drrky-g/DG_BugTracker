using System;
using System.Collections.Generic;
using System.Web;
using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace DG_BugTracker.Helpers
{
    public class HistoryHelper : InstanceHelper
    {
        
        //TODO: Try and find a way to use a foreach loop for the conditional checks instead of doing each one seperately
        public static void CreateHistoryEntries(Ticket origin, Ticket edit)
        {
            //containers that will be used for entries
            var properties = new Stack<string>();
            var oldValues = new Stack<string>();
            var newValues = new Stack<string>();

            //this seperate stack serves as the counter for the others and will clear when it finishes executing
            var ticker = new Stack<int>();

            //compare editable properties. if they change, they will be pushed to the according stack for making entries
            if (origin.Title != edit.Title)
            {
                properties.Push("Title");
                oldValues.Push(origin.Title);
                newValues.Push(edit.Title);
                ticker.Push(1);
            }
            if (origin.Description != edit.Description)
            {
                properties.Push("Description");
                oldValues.Push(origin.Description);
                newValues.Push(edit.Description);
                ticker.Push(1);
            }
            if (origin.TicketTypeId != edit.TicketTypeId)
            {
                properties.Push("Type");
                oldValues.Push(origin.TicketType.Name);
                newValues.Push(edit.TicketType.Name);
                ticker.Push(1);
            }
            if (origin.TicketPriorityId != edit.TicketPriorityId)
            {
                properties.Push("Priority");
                oldValues.Push(origin.TicketType.Name);
                newValues.Push(edit.TicketType.Name);
                ticker.Push(1);
            }
            if (origin.TicketStatusId != edit.TicketStatusId)
            {
                properties.Push("Status");
                oldValues.Push(origin.TicketStatus.Name);
                newValues.Push(edit.TicketType.Name);
                ticker.Push(1);
            }
            if (origin.AssignedToUserId != edit.AssignedToUserId)
            {
                properties.Push("Developer Assignment");
                oldValues.Push(origin.AssignedToUser.FullName);
                newValues.Push(edit.AssignedToUser.FullName);
                ticker.Push(1);
            }

            foreach ( var entry in ticker)
            {
                var newHistory = new TicketHistory
                {
                    Property = properties.Pop(),
                    OldValue = oldValues.Pop(),
                    NewValue = newValues.Pop(),
                    Changed = DateTimeOffset.Now,
                    UserId = HttpContext.Current.User.Identity.GetUserId(),
                    TicketId = edit.Id
                };
                db.TicketHistories.Add(newHistory);
            }
            db.SaveChanges();

            //clear the ticker when all histories are made
            ticker.Clear();
        }




    }
}