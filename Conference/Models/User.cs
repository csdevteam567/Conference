using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Conference.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

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

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }
    }
}