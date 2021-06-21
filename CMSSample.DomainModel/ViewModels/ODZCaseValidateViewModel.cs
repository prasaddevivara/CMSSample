using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel.ViewModels
{
    public class ODZCaseValidateViewModel
    {
        public int ODZCaseID { get; set; }

        public DateTime ValidationDate { get; set; }

        public string ValidationDesc { get; set; }

        public int? ValidatedByUser { get; set; }
    }
}
