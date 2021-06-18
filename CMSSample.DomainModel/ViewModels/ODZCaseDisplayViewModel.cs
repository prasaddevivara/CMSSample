using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel.ViewModels
{
    public class ODZCaseDisplayViewModel
    {
        public int ODZCaseID { get; set; }

        [Display(Name = "Reference Number")]
        public int ODZCaseReference { get; set; }

        [Display(Name = "IncidentType")]
        public string IncidentTypeName { get; set; }

        [Display(Name = "Incident Country")]
        public string DZName { get; set; } // LDZID
        [Display(Name = "Coverage Amount")]
        public int CaseCoverageAmount { get; set; }

        [Display(Name = "Assisted Person")]
        public string AssistedPerson { get; set; }

        [Display(Name = "Case Description")]
        public string CaseDescription { get; set; }
    }
}
