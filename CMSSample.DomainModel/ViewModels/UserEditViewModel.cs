using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMSSample.DomainModel.ViewModels
{
    public class UserEditViewModel
    {
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "User DZ")]
        public IEnumerable<SelectListItem> UserDZs{ get; set; }

        [Required]
        [Display(Name = "User DZ")]
        public int DZId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "User Roles")]
        public int RoleID { get; set; }
        
        [Display(Name ="User Roles")]
        public IEnumerable<SelectListItem> UserRoles { get; set; }
    }
}
