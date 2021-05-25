using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel
{
    public class IncidentType
    {
        [Key]
        public int IncidentTypeID { get; set; }

        [Required]
        [Display(Name = "IncidentType Name")]
        [StringLength(100)]
        public string IncidentTypeName { get; set; }

        public ICollection<ODZCase> ODZCases { get; set; }
    }
}
