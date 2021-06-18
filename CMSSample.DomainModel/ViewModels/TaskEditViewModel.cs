using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CMSSample.DomainModel.ViewModels
{
    public class TaskEditViewModel
    {
        [Required]
        public int TaskId { get; set; }

        public int TaskTypeId { get; set; }
         
        [Display(Name = "Task Type")]
        public IEnumerable<SelectListItem> TaskTypes { get; set; }

        public string TaskDescription { get; set; }

        public int ODZCaseID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? CompletedDate { get; set; }

        public int UserId { get; set; }

    }
}
