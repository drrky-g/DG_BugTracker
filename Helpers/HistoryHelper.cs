using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DG_BugTracker.Models;

namespace DG_BugTracker.Helpers
{
    public class HistoryHelper : InstanceHelper
    {
        //were changes made to the ticket?
        public bool ChangesWereMade(Ticket origin, Ticket edit)
        {
            if(origin != edit)
            {
                return true;
            }
            else
            {
                return false;
            }  
        }
       

        public List<List<string>> OrganizeChangedProperties(Ticket origin, Ticket edit)
        {
            if(ChangesWereMade(origin, edit))
            {
                var master = new List<List<string>>();
                var properties = new List<string>();
                var oldValues = new List<string>();
                var newValues = new List<string>();

                if (origin.Title != edit.Title)
                {
                    properties.Add("Title");
                    newValues.Add(edit.Title);
                    oldValues.Add(origin.Title);
                }
                if (origin.Description != edit.Description)
                {
                    properties.Add("Description");
                    newValues.Add(edit.Description);
                    oldValues.Add(origin.Description);
                }
                if (origin.TicketTypeId != edit.TicketTypeId)
                {
                    properties.Add("Type");
                    newValues.Add(edit.TicketType.Name);
                    oldValues.Add(origin.TicketType.Name);
                }
                if (origin.TicketStatusId != edit.TicketStatusId)
                {
                    properties.Add("Status");
                    newValues.Add(edit.TicketStatus.Name);
                    oldValues.Add(origin.TicketStatus.Name);
                }
                if (origin.TicketPriorityId != edit.TicketPriorityId)
                {
                    properties.Add("Priority");
                    newValues.Add(edit.TicketStatus.Name);
                    oldValues.Add(origin.TicketStatus.Name);
                }
                if (origin.AssignedToUserId != edit.AssignedToUserId)
                {
                    properties.Add("Assigned Developer");
                    newValues.Add(edit.Title);
                    oldValues.Add(origin.Title);
                }
                master.Add(properties);
                master.Add(newValues);
                master.Add(oldValues);

                return master;

            }
            else
            {
                return null;
            }
        }

        //Now that i have my information organized... how will i use it to send a notification and create a history entry?

        public void GenerateHistoryEntries(List<List<string>> master)
        {
            //this will unpackage my master list of lists into seperate lists again
            var properties = master.FirstOrDefault();
            var newValues = master.Skip(1).FirstOrDefault();
            var oldValues = master.Skip(2).FirstOrDefault();



            //generate history entries seperately
            foreach(var item in master)
            {
                var history = new TicketHistory
                {

                };
            }



        }
    }
}