using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        public DbSet<DZ> DZ { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            //modelBuilder.Entity<User>().ToTable("Users");
            //modelBuilder.Entity<DZ>().ToTable("DZ");

            modelBuilder.Entity<DZ>().HasKey(p => p.DZId);
            modelBuilder.Entity<DZ>().Property(c => c.DZId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>().HasKey(b => b.UserId);
            modelBuilder.Entity<User>().Property(b => b.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>().HasRequired<DZ>(p => p.DZ)
                .WithMany(b => b.Users).HasForeignKey<int>(b => b.DZId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
