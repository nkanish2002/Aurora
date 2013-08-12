using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Aurora.Models.Primitives
{
    /// <summary>
    /// Describes a Campus Object and its associated properties
    /// </summary>
    public class Campus
    {
        /// <summary>
        /// Corresponding Id of the Campus
        /// </summary>
        public int CampusId { get; set; }

        /// <summary>
        /// Name of the Campus
        /// </summary>
        public string CampusName { get; set; }
    }

    /// <summary>
    /// Configures the Campus Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class CampusConfiguration : EntityTypeConfiguration<Campus>
    {
        public CampusConfiguration()
        {
            #region Primary Key
            this.HasKey(t => t.CampusId);
            #endregion

            #region Property Constraints
            this.Property(t => t.CampusName)
                .IsRequired()
                .HasMaxLength(11);
            #endregion
        }
    }
}
