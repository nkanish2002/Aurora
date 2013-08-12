using Aurora.Models.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Aurora.Models.Derived
{
    /// <summary>
    /// Describes a Permission Object and its associated properties
    /// A Permission describes access to the Resources based on specific Roles.
    /// </summary>
    public class StudentProgression
    {
        /// <summary>
        /// Profile registerd by the Student
        /// </summary>
        public int HiringId { get; set; }
        public virtual HiringProfile HiringProfile { get; set; }

        
        /// <summary>
        /// Student Details
        /// </summary>
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        /// <summary>
        /// Number of rounds cleared by the student.
        /// Note: Use 1,2,3... in case the system shouldn't target specific rounds cleared.
        /// User Summation of 2^x in case the system should track individual rounds as summation of all subsets
        /// are mutually exclusive
        /// </summary>
        public int Cleared { get; set; }

        /// <summary>
        /// Total number of rounds present in this profile.
        /// Added just to cache data and prevent repetitive calls.
        /// Note: This field must be updated as per the hiring profile
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// Configures the Permission Object and maps respective properties to their Columns.
    /// Also describes relationships to the other tables and constraints on the properties.
    /// </summary>
    public class StudentProgressionConfiguration : EntityTypeConfiguration<StudentProgression>
    {
        public StudentProgressionConfiguration()
        {
            #region Primary Key
            this.HasKey(t => new { t.HiringId, t.StudentId });
            #endregion
        }
    }
}
