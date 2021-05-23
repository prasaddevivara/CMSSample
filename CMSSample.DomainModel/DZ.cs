using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel
{
    public class DZ
    {
        [Key]
        public int DZId { get; set; }

        [Required]
        [Display(Name = "DZ Name")]
        [StringLength(100)]
        public string DZName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
