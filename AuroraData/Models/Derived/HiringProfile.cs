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
    /// Describes a HiringProfile Object and its associated properties
    /// A Hiring Profile contains all the necessary parameters required to determine whether
    /// a candidate is suitable for applying for placement to the Company exposing the 
    /// Hiring Profile
    /// </summary>
    public class HiringProfile
    {
        /// <summary>
        /// Corresponding Id of the Hiring Profile
        /// </summary>
        public int HiringProfileId { get; set; }

        /// <summary>
        /// Company to which this Hiring Profile Corresponds to.
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// Campus where the Hiring will take place
        /// </summary>
        public Campus VisitingCampus { get; set; }

        /// <summary>
        /// Departments to which this profile is applicable
        /// </summary>
        public ICollection<Department> Departments { get; set; }

        /// <summary>
        /// Process Steps in this profile
        /// </summary>
        public ICollection<ProcessStep> ProcessSteps { get; set; }

        /// <summary>
        /// Registered Students for this Hiring Profile
        /// </summary>
        public ICollection<StudentProgression> RegisteredStudents { get; set; }

        /// <summary>
        /// A User readable name assigned to the profile
        /// </summary>
        public string HiringProfileName { get; set; }

        /// <summary>
        /// The batch of the students to whom this profile applies
        /// </summary>
        public int Batch { get; set; }

        /// <summary>
        /// Minimum Tenth Percentage required
        /// </summary>
        public float TenthPercentage { get; set; }

        /// <summary>
        /// Minimum Twelvth Percentage required
        /// </summary>
        public float TwelvthPercentage { get; set; }

        /// <summary>
        /// Minimum CGPA required
        /// </summary>
        public float CGPA { get; set; }

        /// <summary>
        /// Minimum UnderGraduate Percentage required [Optional]
        /// </summary>
        public float UnderGraduatePercentage { get; set; }

        /// <summary>
        /// Maximum Standing Arrears allowed
        /// </summary>
        public int StandingArrears { get; set; }

        /// <summary>
        /// Maximum Historical Arrears allowed
        /// </summary>
        public int HistoricalArrears { get; set; }

        /// <summary>
        /// Job Profile which will be assigned to selected candidates
        /// </summary>
        public string JobProfile { get; set; }

        /// <summary>
        /// UnderGraduate Cost To Company offered
        /// </summary>
        public float UnderGraduateCTC { get; set; }

        /// <summary>
        /// PostGraduate Cost To Company offered [Optional]
        /// </summary>
        public float PostGraduateCTC { get; set; }

        /// <summary>
        /// Stipend offered during Internship [Optional]
        /// </summary>
        public float InternshipStipend { get; set; }

        /// <summary>
        /// Date on which Hiring will commence
        /// </summary>
        public DateTime HiringDate { get; set; }

        /// <summary>
        /// Additional Information related to this profile
        /// </summary>
        public string Comments { get; set; }
    }

    /// <summary>
    /// Configures the Hiring Profile Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class HiringProfileConfiguration : EntityTypeConfiguration<HiringProfile>
    {
        public HiringProfileConfiguration()
        {
            #region Primary Key
            this.HasKey(t => t.HiringProfileId);
            #endregion

            #region Property Constraints
            this.Property(t => t.HiringProfileName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Batch)
                .IsRequired();

            this.Property(t => t.TenthPercentage)
                .IsRequired();
                
            this.Property(t => t.TwelvthPercentage)
                .IsRequired();

            this.Property(t => t.CGPA)
                .IsRequired();

            this.Property(t => t.JobProfile)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UnderGraduateCTC)
                .IsRequired();

            this.Property(t => t.HiringDate)
                .IsRequired();

            this.Property(t => t.Comments)
                .HasMaxLength(300);
            #endregion

            #region Foreign Keys
            this.HasRequired(t => t.VisitingCampus)
                .WithMany()
                .Map(t => t.MapKey("CampusId"));
                

            this.HasMany(t => t.Departments)
                .WithMany()
                .Map(
                    t => t
                        .ToTable("HiringFrom")
                        .MapLeftKey("HiringProfileId")
                        .MapRightKey("DepartmentId")
                );

            this.HasMany(t => t.ProcessSteps)
                .WithMany()
                .Map(
                    t => t
                        .ToTable("HiringProcess")
                        .MapLeftKey("HiringProfileId")
                        .MapRightKey("ProcessStepId")
                );

            this.HasMany(t => t.RegisteredStudents)
                .WithRequired(t => t.HiringProfile)
                .HasForeignKey(t => t.HiringId)
                .WillCascadeOnDelete();
            #endregion
        }
    }
}
