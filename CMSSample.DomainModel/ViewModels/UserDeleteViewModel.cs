using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel.ViewModels
{
    public class UserDeleteViewModel
    {
        public int UserId { get; set; }

        public DateTime DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
