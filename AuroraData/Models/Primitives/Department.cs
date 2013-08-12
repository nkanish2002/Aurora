using Aurora.Models.Derived;
using Aurora.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Aurora.Models.Primitives
{
    /// <summary>
    /// Describes a Department Object and its associated properties
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Corresponding Id of the Department
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Name of the Department
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Determines if the Department is an Undergraduate Course or PostGraduate Course
        /// </summary>
        public bool IsUndergraduate { get; set; }
    }

    /// <summary>
    /// Configures the Department Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            #region Primary Key
            this.HasKey(t => t.DepartmentId);
            #endregion

            #region Property Constraints
            this.Property(t => t.DepartmentName)
                .IsRequired()
                .HasMaxLength(50);
            #endregion

        }
    }
}
