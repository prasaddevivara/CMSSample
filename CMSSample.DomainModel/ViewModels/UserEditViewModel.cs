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

        public string Password { get; set; }

        [Display(Name = "User DZ")]
        public IEnumerable<SelectListItem> UserDZs{ get; set; }

        [Required]
        public int DZId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public int RoleID { get; set; }
        [Required]
        [Display(Name ="User Roles")]
        public IEnumerable<SelectListItem> UserRoles { get; set; }
    }
}
