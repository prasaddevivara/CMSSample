using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using CMSSample.DomainModel;

namespace CMSSample.DomainModel
{
    public class TaskType
    {
        [Required]
        [Key]
        public int TaskTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string TaskTypeName { get; set; }

        
        public virtual ICollection<Task> Tasks { get; set; }

    }
}
