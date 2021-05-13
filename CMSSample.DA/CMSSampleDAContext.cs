using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DA
{
    public class CMSSampleDAContext : DbContext
    {
      public CMSSampleDAContext()   : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
