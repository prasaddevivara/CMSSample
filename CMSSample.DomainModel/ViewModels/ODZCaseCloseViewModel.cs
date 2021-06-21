using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel.ViewModels
{
    public class ODZCaseCloseViewModel
    {
        public int ODZCaseID { get; set; }

        public DateTime ClosedByDate { get; set; }

        public string ClosingDesc { get; set; }

        public int? ClosedByuser { get; set; }
    }
}
