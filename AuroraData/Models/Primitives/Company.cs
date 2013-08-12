using Aurora.Models.Derived;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Models.Primitives
{
    /// <summary>
    /// Describes a Company Object and its associated properties
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Corresponding Id of the Company
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Name of the Company
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Details about the Company
        /// </summary>
        public string CompanyDescription { get; set; }

        /// <summary>
        /// Address of the Company
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// Website of the Company
        /// </summary>
        public string CompanyUrl { get; set; }

        /// <summary>
        /// Link to the Facebook or LinkedIn profile page. 
        /// Can be queried for additional information and resources
        /// </summary>
        public string CompanyProfile { get; set; }

        /// <summary>
        /// List of Hiring Profiles associated to the Company
        /// </summary>
        public ICollection<HiringProfile> HiringProfiles { get; set; }
    }

    /// <summary>
    /// Configures the Company Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            #region Primary Key
            this.HasKey(t => t.CompanyId);
            #endregion

            #region Property Constraints
            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyDescription)
                .IsRequired();

            this.Property(t => t.CompanyUrl)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CompanyProfile)
                .HasMaxLength(100);

            this.Property(t => t.CompanyAddress)
                .IsRequired()
                .HasMaxLength(300);
            #endregion

            #region Foreign Keys
            this.HasMany(t => t.HiringProfiles)
                .WithRequired(t => t.Company)
                .Map(t => t.MapKey("CompanyId"));
            #endregion
        }
    }
}
