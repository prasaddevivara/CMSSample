using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel.ViewModels
{
    public class TaskDisplayViewModel
    {
        [Required]
        public int TaskId { get; set; }

        public int TaskTypeId { get; set; }

        [Display(Name = "TaskType")]
        [Required]
        [StringLength(100)]
        public string TaskTypeName { get; set; }

        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; }
                
        public int ODZCaseID { get; set; }

        [Display(Name = "Ref. Number")]

        public int ODZCaseReference { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string CompletedDate { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Created User")]
        public string UserName { get; set; }
    }
}
