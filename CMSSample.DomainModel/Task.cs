using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel
{
    public class Task
    {
        [Key]
        [Required]
        public int TaskId { get; set; }

        [Required]
        public int TaskTypeID { get; set; }

        public string TaskDescription { get; set; }
        
        [Required]
        public int ODZCaseID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        
        [Display(Name = "CreatedUser")]
        public int UserId { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual User User { get; set;}
       
        public virtual ODZCase ODZCase { get; set; }
        
        public virtual TaskType TaskType { get; set; }        
    }
}
