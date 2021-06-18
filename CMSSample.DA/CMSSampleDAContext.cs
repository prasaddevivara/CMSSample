using CMSSample.DomainModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

using CMSSample.DomainModel;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CMSSample.DA
{
    public class CMSSampleDAContext : DbContext
    {
      public CMSSampleDAContext()   : base("DefaultConnection")
        {
            //Disable initializer You can turn off the database initializer for your application. Suppose that you don't want to lose existing data in the production environment, then you can turn off the initializer, as shown below:
            //Database.SetInitializer(new NullDatabaseInitializer<CMSSampleDAContext>());
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> User { get; set; }

        public DbSet<DZ> DZ { get; set; }

        public DbSet<IncidentType> IncidentType { get; set; }

        public DbSet<ODZCase> ODZCase { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<Task> Task { get; set; }

        public DbSet<TaskType> TaskType { get; set; }

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

            modelBuilder.Entity<UserRoles>().HasKey(u => u.UserRoleID);
            modelBuilder.Entity<UserRoles>().Property(b => b.UserRoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ODZCase>().HasKey(b => b.ODZCaseID);
            modelBuilder.Entity<ODZCase>().Property(b => b.ODZCaseID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Task>().HasKey(b => b.TaskId);
            modelBuilder.Entity<Task>().Property(b => b.TaskId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>().HasRequired<DZ>(p => p.DZ)
                .WithMany(b => b.Users).HasForeignKey(b => b.DZId).WillCascadeOnDelete(false);
                

            modelBuilder.Entity<ODZCase>().HasRequired<IncidentType>(p => p.IncidentType)
            .WithMany(b => b.ODZCases).HasForeignKey<int>(b => b.IncidentTypeID).WillCascadeOnDelete(false);

            modelBuilder.Entity<ODZCase>().HasRequired<DZ>(p => p.DZ)
                .WithMany(b => b.ODZCases).HasForeignKey<int>(b => b.CountryofIncidentID).WillCascadeOnDelete(false);


            modelBuilder.Entity<User>().HasRequired<UserRoles>(p => p.UserRoles)
                .WithMany(b => b.Users).HasForeignKey<int>(b => b.RoleID).WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>().HasRequired<TaskType>(p => p.TaskType)
                .WithMany(b => b.Tasks).HasForeignKey<int>(b => b.TaskTypeID).WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>().HasRequired<User>(p => p.User)
                .WithMany(b => b.Tasks).HasForeignKey<int>(b => b.UserId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>().HasRequired<ODZCase>(p => p.ODZCase)
                .WithMany(b => b.Tasks).HasForeignKey<int>(b => b.ODZCaseID).WillCascadeOnDelete(false);

            

            //foreach (var relationship in modelBuilder.Entity<User>.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            base.OnModelCreating(modelBuilder);

            
            

        }
    }
}
