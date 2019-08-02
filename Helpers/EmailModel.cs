using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Helpers
{
    public class EmailModel
    {
        [Required]
        [Display (Name = "Name")]
        public string FromName { get; set; }

        [Required]
        [EmailAddress]
        [Display (Name = "Email")]
        public string FromEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [Display (Name = "Body")]
        public string EmailBody { get; set; }
    }
}