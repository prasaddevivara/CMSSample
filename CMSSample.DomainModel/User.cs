using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "UserName")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "FirstName")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        [StringLength(150)]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        [StringLength(50)]
        public string Mobile { get; set; }


        public int DZId { get; set; }

        //[ForeignKey("DZId")]
        public DZ DZ { get; set; }

    }
}
