using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DG_BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Project Description")]
        public string Description { get; set; }
        [Required]
        [Display (Name = "Created")]
        public DateTimeOffset Created { get; set; }

        //------id targetting strings--------------
        
        
        [NotMapped]
        public string BtnTarget
        {
            get
            {
                return $"p{Id}btn";
            }
        }
        [NotMapped]
        public string IdTarget
        {
            get
            {
                return $"p{Id}Id";
            }
        }
        [NotMapped]
        public string NameTarget
        {
            get
            {
                return $"p{Id}Name";
            }
        }
        [NotMapped]
        public string DescTarget
        {
            get
            {
                return $"p{Id}Desc";
            }
        }
        [NotMapped]
        public string CreatedTarget
        {
            get
            {
                return $"p{Id}Created";
            }
        }
        //------------virtual----------------------------
        //------------icollection------------------------
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        //---------constructor---------------------------
        public Project()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Tickets = new HashSet<Ticket>();
        }
    }
}