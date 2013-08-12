using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Aurora.Models.Primitives
{
    /// <summary>
    /// Describes a User Object and its associated properties
    /// A User contains all the information vital to uniquely identify 
    /// and correspond with. Every User must have a Role to access the system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Corresponding Id of the User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Role assigned to the User
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Department of the User
        /// </summary>
        public Department Department { get; set; }
        
        /// <summary>
        /// Campus of the User
        /// </summary>
        public Campus Campus { get; set; }
        
        /// <summary>
        /// Username of the User either chosen automatically during OAuth or assigned manually by the User
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// First Name of the User
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Last Name of the User which includes the
        ///     - Middle Name
        ///     - Initial
        ///     - Family Name or Surname
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Registration Number assigned to the User
        /// </summary>
        public string RegistrationNumber { get; set; }
        
        /// <summary>
        /// Email of the User either chosen automatically during OAuth or assigned manually by the User
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Contact of the User either chosen automatically during OAuth or assigned manually by the User
        /// </summary>
        public string Contact { get; set; }
        
        /// <summary>
        /// Online Profile Link of the User either chosen automatically during OAuth or assigned manually by the User.
        /// Typically a Link to the Facebook or LinkedIn profile page. 
        /// Can be queried for additional information and resources
        /// </summary>
        public string ProfileLink { get; set; }       
    }

    /// <summary>
    /// Configures the User Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            #region Primary Key
            this.HasKey(t => t.UserId);
            #endregion

            #region Property Constraints
            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RegistrationNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Contact)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProfileLink)
                .IsRequired()
                .HasMaxLength(100);
            #endregion

            #region Foreign Keys
            this.HasOptional(t => t.Role)
                .WithMany()
                .Map(t => t.MapKey("RoleId"));

            this.HasOptional(t => t.Campus)
                .WithMany()
                .Map(t => t.MapKey("CampusId"));

            this.HasOptional(t => t.Department)
                .WithMany()
                .Map(t => t.MapKey("DepartmentId"));
            #endregion
        }
    }
}
