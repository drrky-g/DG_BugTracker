using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DG_BugTracker.Models;

namespace DG_BugTracker.ViewModels
{
    public class MyDashboard
    {
        //List of projects where your userId is a member of  ICol<Users> of project

            public List<Project> MyProjects { get; set; }

        //List of tickets

            //If user is Dev, this list will be of tickets where their userId = assignedUserId

            //if user is Submitter, this list will be of tickets where their userId = ownerId

            //If user is PM, this list will be of tickets where they belong
            //to the project which has the same id as this tickets projectId property

            public List<Ticket> MyTickets { get; set; }




    }
}