using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Aurora.Models.Primitives;
using Aurora.Models.Derived;
using System;

namespace Aurora.Entity
{
    public class AuroraContext : DbContext
    {
        static AuroraContext()
        {
            Database.SetInitializer<AuroraContext>(null);
        }

        public AuroraContext()
            : base("Name=AuroraDb")
        {
        }

        #region Queryable Resources

        public DbSet<Campus> Campuses { get; set; }
        
        public DbSet<Department> Departments { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Resource> Resources { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<HiringProfile> HiringProfiles { get; set; }

        public DbSet<ProcessStep> ProcessSteps { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentProgression> StudentProgressions { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Table Configurations
            modelBuilder.Configurations.Add(new CampusConfiguration());
        
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
            
            modelBuilder.Configurations.Add(new RoleConfiguration());

            modelBuilder.Configurations.Add(new ResourceConfiguration());
            
            modelBuilder.Configurations.Add(new PermissionConfiguration());
            
            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Configurations.Add(new CompanyConfiguration());

            modelBuilder.Configurations.Add(new HiringProfileConfiguration());

            modelBuilder.Configurations.Add(new ProcessStepConfiguration());

            modelBuilder.Configurations.Add(new StudentConfiguration());

            modelBuilder.Configurations.Add(new StudentProgressionConfiguration());
            #endregion
        }
    }
}
