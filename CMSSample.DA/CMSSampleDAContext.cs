using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
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
            //Disable initializer You can turn off the database initializer for your application. Suppose that you don't want to lose existing data in the production environment, then you can turn off the initializer, as shown below:
            Database.SetInitializer(new NullDatabaseInitializer<CMSSampleDAContext>());
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<DZ> DZ { get; set; }

        public DbSet<IncidentType> IncidentType { get; set; }

        public DbSet<ODZCase> ODZCase { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DZ>().HasKey(p => p.DZId);
            modelBuilder.Entity<DZ>().Property(c => c.DZId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>().HasKey(b => b.UserId);
            modelBuilder.Entity<User>().Property(b => b.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<IncidentType>().HasKey(b => b.IncidentTypeID);
            modelBuilder.Entity<IncidentType>().Property(b => b.IncidentTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<IncidentType>().HasKey(b => b.IncidentTypeID);
            modelBuilder.Entity<IncidentType>().Property(b => b.IncidentTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ODZCase>().HasKey(b => b.ODZCaseID);
            modelBuilder.Entity<ODZCase>().Property(b => b.ODZCaseID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>().HasRequired<DZ>(p => p.DZ)
                .WithMany(b => b.Users).HasForeignKey<int>(b => b.DZId);

            modelBuilder.Entity<ODZCase>().HasRequired<IncidentType>(p => p.IncidentType)
            .WithMany(b => b.ODZCases).HasForeignKey<int>(b => b.IncidentTypeID);

            modelBuilder.Entity<ODZCase>().HasRequired<DZ>(p => p.DZ)
                .WithMany(b => b.ODZCases).HasForeignKey<int>(b => b.CountryofIncidentID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
