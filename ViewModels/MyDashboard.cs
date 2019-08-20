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
        public ICollection<Project> MyProjects { get; set; }

        //List of tickets
        public ICollection<Ticket> MyTickets { get; set; }



            

            




    }
}