using Aurora.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Models.Derived
{
    /// <summary>
    /// Describes a Student Object and its associated properties
    /// A Student contains all the necessary parameters defined in its base class and also defines
    /// the additional parameters that qualify him to be a student for placement.
    /// </summary>
    public class Student : User
    {
        /// <summary>
        /// Company Details if Student is placed
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// Registered Hiring Profies for this Student
        /// </summary>
        public ICollection<StudentProgression> HiringProfiles { get; set; }

        /// <summary>
        /// Tenth Percentage of the Student
        /// </summary>
        public float TenthPercentage { get; set; }

        /// <summary>
        /// Twelvth Percentage of the Student
        /// </summary>
        public float TwelvthPercentage { get; set; }

        /// <summary>
        /// CGPA of the Student
        /// </summary>
        public float CGPA { get; set; }

        /// <summary>
        /// UnderGraduate Percentage of the Student [Optional]
        /// </summary>
        public float UnderGraduatePercentage { get; set; }

        /// <summary>
        /// Standing Arrears of the Student
        /// </summary>
        public int StandingArrears { get; set; }

        /// <summary>
        /// Historical Arrears of the Student
        /// </summary>
        public int HistoricalArrears { get; set; }
    }

    /// <summary>
    /// Configures the Student Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            #region Table Definition
            this.ToTable("Students");
            #endregion

            #region Property Constraints
            this.Property(t => t.TenthPercentage)
                .IsRequired();
                
            this.Property(t => t.TwelvthPercentage)
                .IsRequired();

            this.Property(t => t.CGPA)
                .IsRequired();
            #endregion

            #region Foreign Keys
            this.HasOptional(t => t.Company)
                .WithOptionalDependent()
                .Map(t => t.MapKey("CompanyId"));

            this.HasMany(t => t.HiringProfiles)
                .WithRequired(t => t.Student)
                .HasForeignKey(t => t.StudentId)
                .WillCascadeOnDelete();
            #endregion
        }
    }
}
