using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Conference.Models
{
    public class UsersViewModel
    {
        [Display(Name = "Full name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Age")]
        [Required]
        public int Age { get; set; }

        [Display(Name = "eMail")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        [Display(Name = "State capital")]
        public string StateCapital { get; set; }
    }
}