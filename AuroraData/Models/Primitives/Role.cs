using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Aurora.Models.Derived;

namespace Aurora.Models.Primitives
{
    /// <summary>
    /// Describes a Role Object and its associated properties
    /// A Role dictates the privilages available to the person assigned with the
    /// respective role.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Corresponding Id of the Role
        /// </summary>
        public int RoleId { get; set; }
        
        /// <summary>
        /// Name of the Role
        /// </summary>
        public string RoleName { get; set; }
    }

    /// <summary>
    /// Configures the Role Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            #region Primary Key
            this.HasKey(t => t.RoleId);
            #endregion

            #region Property Constraints
            this.Property(t => t.RoleName)
                .IsRequired()
                .HasMaxLength(30);
            #endregion
        }
    }
}
