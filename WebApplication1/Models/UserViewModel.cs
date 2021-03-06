using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSSample.DomainModel;

namespace WebApplication1.Models
{
    public class UserViewModel
    {       
        public int UserId { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }       
        public string Mobile { get; set; }

        [Display(Name = "DZ")]
        public int DZId { get; set; }

        [Display(Name ="DZ Name")]
        public int DZName { get; set; }

        public IEnumerable<DZ> DZ { get; set; }


    }
}