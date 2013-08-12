using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Aurora.Models.Primitives
{
    /// <summary>
    /// Describes a Proces Step Object and its associated properties.
    /// A process step is a generic evaluation type that is performed in the hiring process.
    /// </summary>
    public class ProcessStep
    {
        /// <summary>
        /// Corresponding Id of the Process Step
        /// </summary>
        public int ProcessStepId { get; set; }

        /// <summary>
        /// Name of the Process Step
        /// </summary>
        public string ProcessStepName { get; set; }
    }

    /// <summary>
    /// Configures the Process Step Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class ProcessStepConfiguration : EntityTypeConfiguration<ProcessStep>
    {
        public ProcessStepConfiguration()
        {
            #region Primary Key
            this.HasKey(t => t.ProcessStepId);
            #endregion

            #region Property Constraints
            this.Property(t => t.ProcessStepName)
                .IsRequired()
                .HasMaxLength(30);
            #endregion
        }
    }
}
