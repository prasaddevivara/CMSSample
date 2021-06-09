using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DomainModel
{
    public class UserRoles
    {
        [Key]
        public int UserRoleID { get; set; }

        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
