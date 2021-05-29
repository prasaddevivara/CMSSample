using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel.ViewModels
{
    public class UserDisplayViewModel
    {
        public string UserName { get; set; }  

        [Display(Name = "User DZ")]
        public string DZName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


    }
}
