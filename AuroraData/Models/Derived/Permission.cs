using Aurora.Models.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Aurora.Models.Derived
{
    /// <summary>
    /// Describes a Permission Object and its associated properties
    /// A Permission describes access to the Resources based on specific Roles.
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Corresponding Id of the Permission
        /// </summary>
        public int PermissionId { get; set; }
        
        /// <summary>
        /// Resource allocated to the User
        /// </summary>
        public Resource Resource { get; set; }
        
        /// <summary>
        /// Role to which the permission id applied
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Permission to CREATE a new Resource by the Role
        /// </summary>
        public bool CanCreate { get; set; }
        
        /// <summary>
        /// Permission to READ an existing Resource by the Role
        /// </summary>
        public bool CanRead { get; set; }
        
        /// <summary>
        /// Permission to UPDATE an existing Resource by the Role
        /// </summary>
        public bool CanUpdate { get; set; }
        
        /// <summary>
        /// Permission to DELETE and existing Resource by the Role
        /// </summary>
        public bool CanDelete { get; set; }
    }

    /// <summary>
    /// Configures the Permission Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class PermissionConfiguration : EntityTypeConfiguration<Permission>
    {
        public PermissionConfiguration()
        {
            #region Primary Key
            this.HasKey(t => t.PermissionId);
            #endregion

            #region Foreign Keys
            this.HasRequired(t => t.Role)
                .WithMany()
                .Map(t => t.MapKey("RoleId"));

            this.HasRequired(t => t.Resource)
                .WithMany()
                .Map(t => t.MapKey("ResourceId"));
            #endregion
        }
    }
}
