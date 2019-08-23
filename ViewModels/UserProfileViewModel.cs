using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DG_BugTracker.Models;

namespace DG_BugTracker.ViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(25), MinLength(1)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(40), MinLength(1)]
        public string LastName { get; set; }

        [Display(Name = "Profile Picture")]
        public string AvatarPath { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> CurrentRole { get; set; }
        public IEnumerable<SelectListItem> CurrentProjects { get; set; }

    }
}