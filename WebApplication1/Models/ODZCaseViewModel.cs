using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMSSample.DomainModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class ODZCaseViewModel
    {
        public int ODZCaseID { get; set; }

        [Required]
        [Display(Name = "ODZCase Reference")]
        public int ODZCaseReference { get; set; }

        [ForeignKey("IncidentType")]
        public int IncidentTypeID { get; set; }

        public string IncidentTypeName { get; set; }
        [ForeignKey("DZ")]
        public int CountryofIncidentID { get; set; } // LDZName

        public string DZName { get; set; }

        [Required]
        [Display(Name = "Case Coverage Amount")]
        public int CaseCoverageAmount { get; set; }

        public string AssistedPerson { get; set; }

        public string CaseDescription { get; set; }

        public IEnumerable<IncidentType> IncidentType { get; set; }

        public IEnumerable<DZ> DZ { get; set; }
    }
}