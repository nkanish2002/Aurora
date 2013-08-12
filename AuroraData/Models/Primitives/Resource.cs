using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Models.Primitives
{
    /// <summary>
    /// Describes a Resource Object and its associated properties
    /// A Resource is defined as anything that can be queried for, 
    /// from the system or an external agent.
    /// e.g. Company, User, Campus etc.
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// Corresponding Id of the Role
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Name of the Resource
        /// </summary>
        public string ResourceName { get; set; }
    }

    /// <summary>
    /// Configures the Resource Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class ResourceConfiguration : EntityTypeConfiguration<Resource>
    {
        public ResourceConfiguration()
        {
            #region Primary Key
            this.HasKey(t => t.ResourceId);
            #endregion

            #region Property Constraints
            this.Property(t => t.ResourceName)
                .IsRequired()
                .HasMaxLength(50);
            #endregion
        }
    }
}
