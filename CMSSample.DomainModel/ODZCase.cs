using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using CMSSample.DomainModel;

namespace CMSSample.DomainModel
{
    public class ODZCase
    {
        [Key]
        public int ODZCaseID { get; set; }

        public int ODZCaseReference { get; set; }

        [Required]
        public int IncidentTypeID { get; set; } 

        public int CountryofIncidentID { get; set; } // LDZID

        public int CaseCoverageAmount { get; set; }

        public string AssistedPerson { get; set; }

        public string CaseDescription { get; set; }

        public virtual IncidentType IncidentType { get; set; }

        public virtual DZ DZ { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

    }
}
