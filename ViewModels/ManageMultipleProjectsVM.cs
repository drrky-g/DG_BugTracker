using DG_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DG_BugTracker.ViewModels
{
    public class ManageMultipleProjectsVM : UserProfileViewModel
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public  MultiSelectList ProjectSelect {get; set;}
    }
}