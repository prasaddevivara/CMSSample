using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMSSample.DomainModel.ViewModels
{
    public class ODZCaseEditViewModel
    {
        public int ODZCaseID { get; set; }

        public int ODZCaseReference { get; set; }

        [Required]
        [Display(Name = "IncidentType")]
        public string IncidentTypeID { get; set; }

        public IEnumerable<SelectListItem> IncidentTypes { get; set; }

        [Required]
        [Display(Name = "CountryOfIncident")]
        public int SelectedCountryofIncidentID { get; set; }

        public IEnumerable<SelectListItem> DZS { get; set; }

        public int CaseCoverageAmount { get; set; }

        public string AssistedPerson { get; set; }

        public string CaseDescription { get; set; }
        
    }
}
