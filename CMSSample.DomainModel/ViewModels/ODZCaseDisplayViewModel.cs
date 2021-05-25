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

        public int ODZCaseReference { get; set; }

        public string IncidentTypeName { get; set; }

        [Display(Name = "CountryOfIncident")]
        public string DZName { get; set; } // LDZID

        public int CaseCoverageAmount { get; set; }

        public string AssistedPerson { get; set; }

        public string CaseDescription { get; set; }
    }
}
