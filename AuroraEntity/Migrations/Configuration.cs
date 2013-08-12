namespace Aurora.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Aurora.Models.Derived;
    using Aurora.Models.Primitives;
    using Aurora.Entity;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<AuroraContext>
    {

        #region Campus Seed Values
        private Campus Bangalore = new Campus { CampusId = 1, CampusName = "Bangalore" };

        private Campus Coimbatore = new Campus { CampusId = 2, CampusName = "Coimbatore" };

        private Campus Amritapuri = new Campus { CampusId = 3, CampusName = "Amritapuri" };

        private Campus Mysore = new Campus { CampusId = 4, CampusName = "Mysore" };

        private Campus Kochi = new Campus { CampusId = 5, CampusName = "Kochi" };
        #endregion

        #region Role Seed Values
        private Role Developer = new Role { RoleId = 1, RoleName = "Developer" };

        private Role Admin = new Role { RoleId = 2, RoleName = "Administrator" };

        private Role SuperModerator = new Role { RoleId = 3, RoleName = "SuperModerator" };

        private Role Moderator = new Role { RoleId = 4, RoleName = "Moderator" };

        private Role User = new Role { RoleId = 5, RoleName = "User" };
        #endregion

        #region Resource Seed Values
        private Resource Resource = new Resource { ResourceId = 1, ResourceName = "Resource Management" };

        private Resource Role = new Resource { ResourceId = 2, ResourceName = "Role Management" };

        private Resource Campus = new Resource { ResourceId = 3, ResourceName = "Campus Management" };

        private Resource Department = new Resource { ResourceId = 4, ResourceName = "Department Management" };

        private Resource Permission = new Resource { ResourceId = 5, ResourceName = "Permission Management" };
        #endregion

        #region Department Seed Values
        private Department CSE = new Department { DepartmentId = 1, DepartmentName = "Computer Science", IsUndergraduate = true };

        private Department ECE = new Department { DepartmentId = 2, DepartmentName = "Electronics and Communication", IsUndergraduate = true };

        private Department EEE = new Department { DepartmentId = 3, DepartmentName = "Electrical and Electronics", IsUndergraduate = true };

        private Department EIE = new Department { DepartmentId = 4, DepartmentName = "Electronics and Instrumentation", IsUndergraduate = true };

        private Department CIVIL = new Department { DepartmentId = 5, DepartmentName = "Civil", IsUndergraduate = true };

        private Department MECH = new Department { DepartmentId = 6, DepartmentName = "Mechanical", IsUndergraduate = true };
        #endregion

        #region Process Step Seed Values
        ProcessStep Written = new ProcessStep { ProcessStepName = "Written Test" };

        ProcessStep TechnicalRound = new ProcessStep { ProcessStepName = "Technical Interview" };

        ProcessStep HRRound = new ProcessStep { ProcessStepName = "HR Interview" };
        #endregion

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuroraContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            InitialPrimitivesSeed(context);

            context.SaveChanges();

            PermissionModelSeed(context);

            context.SaveChanges();

            UserModelSeed(context);

            context.SaveChanges();

            CompanyModelSeed(context);

            context.SaveChanges();

            HiringProcessSeed(context);

            context.SaveChanges();

            StudentModelSeed(context);

            context.SaveChanges();

            ProgressionModelSeed(context);

            context.SaveChanges();

        }

        private void InitialPrimitivesSeed(AuroraContext context)
        {

            context.Campuses.AddOrUpdate(t => t.CampusName, Bangalore, Coimbatore, Amritapuri, Mysore, Kochi);

            context.Roles.AddOrUpdate(t => t.RoleName, Developer, Admin, SuperModerator, Moderator, User);

            context.Resources.AddOrUpdate(t => t.ResourceName, Resource, Role, Campus, Department, Permission);

            context.Departments.AddOrUpdate(t => t.DepartmentName, CSE, EEE, ECE, EIE, MECH, CIVIL);

        }

        private void PermissionModelSeed(AuroraContext context)
        {
            var Developer = context.Roles.First(t => t.RoleId == 1);
            var Admin = context.Roles.First(t => t.RoleId == 2);

            var Resource = context.Resources.First(t => t.ResourceId == 1);
            var Role = context.Resources.First(t => t.ResourceId == 2);
            var Campus = context.Resources.First(t => t.ResourceId == 3);
            var Department = context.Resources.First(t => t.ResourceId == 4);
            var Permission = context.Resources.First(t => t.ResourceId == 5);

            if (new object[] { Developer, Admin, Resource, Role, Campus, Department, Permission }.All(t => t != null))
            {
                context.Permissions.AddOrUpdate(
                    t => t.PermissionId,
                    new Permission { PermissionId = 1, Role = Developer, Resource = Resource, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true },
                    new Permission { PermissionId = 2, Role = Developer, Resource = Role, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true },
                    new Permission { PermissionId = 3, Role = Developer, Resource = Campus, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true },
                    new Permission { PermissionId = 4, Role = Developer, Resource = Department, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true },
                    new Permission { PermissionId = 5, Role = Developer, Resource = Permission, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true },

                    new Permission { PermissionId = 6, Role = Admin, Resource = Resource, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true },
                    new Permission { PermissionId = 7, Role = Admin, Resource = Role, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true },
                    new Permission { PermissionId = 8, Role = Admin, Resource = Campus, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true },
                    new Permission { PermissionId = 9, Role = Admin, Resource = Department, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true },
                    new Permission { PermissionId = 10, Role = Admin, Resource = Permission, CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true }
                );
            }
        }

        private void UserModelSeed(AuroraContext context) {
            var Campus = context.Campuses.Find(1);
            var Department = context.Departments.Find(1);
            var Role = context.Roles.Find(1);

            context.Users.AddOrUpdate(
                t => t.UserId,
                new User { 
                    UserId = 1,
                    Campus = Campus,
                    Department = Department,
                    Role = Role,
                    Username = "warlord1609", 
                    FirstName = "Bhargav",
                    LastName = "Krishna",
                    RegistrationNumber = "BL.EN.U4CSE09022",
                    Email = "bhargavkrishna16@gmail.com",
                    Contact = "+919844469276",
                    ProfileLink = "https://www.facebook.com/GeekInfinite"
                },
                new User
                {
                    UserId = 2,
                    Campus = Campus,
                    Department = Department,
                    Role = Role,
                    Username = "amritha.dilip",
                    FirstName = "Amritha",
                    LastName = "Dilip",
                    RegistrationNumber = "BL.EN.U4CSE09501",
                    Email = "amritha.dilip@gmail.com",
                    Contact = "+917411099809",
                    ProfileLink = "https://www.facebook.com/GeekInfinite"
                },
                new User
                {
                    UserId = 3,
                    Campus = Campus,
                    Department = Department,
                    Role = Role,
                    Username = "aishwarya1411",
                    FirstName = "Aishwarya",
                    LastName = "Ramanathan",
                    RegistrationNumber = "BL.EN.U4CSE09004",
                    Email = "aishwarya1411@gmail.com",
                    Contact = "+917411099809",
                    ProfileLink = "https://www.facebook.com/GeekInfinite"
                }
            );
        }

        private void CompanyModelSeed(AuroraContext context)
        {
            var CSE = context.Departments.Find(1);
            var ECE = context.Departments.Find(2);
            var Bangalore = context.Campuses.Find(1);

            HiringProfile FacebookHiringProfile = new HiringProfile
            {
                Batch = 2013,
                CGPA = 6.0f,
                Comments = null,
                Departments = new List<Department> { CSE, ECE } as ICollection<Department>,
                HiringDate = DateTime.Now.AddMonths(5),
                HiringProfileName = "Facebook Hiring BTech 2013",
                TenthPercentage = 60,
                TwelvthPercentage = 60,
                UnderGraduateCTC = 1200000,
                VisitingCampus = Bangalore,
                JobProfile = "Junior Web Developer",
            };

            Company FacebookIndia = new Company
            {
                CompanyName = "Facebook India",
                CompanyProfile = "https://www.facebook.com/FacebookIndia",
                CompanyUrl = "https://www.facebook.com",
                CompanyDescription = "Facebook is an online social networking service, whose name stems from the colloquial name for the book given to students at the start of the academic year by some university administrations in the United States to help students get to know each other.\r\n"
                                + "It was founded in February 2004 by Mark Zuckerberg with his college roommates and fellow Harvard University students Eduardo Saverin, Andrew McCollum, Dustin Moskovitz and Chris Hughes. The website's membership was initially limited by the founders to Harvard students, but was expanded to other colleges in the Boston area, the Ivy League, and Stanford University.\r\n"
                                + "It gradually added support for students at various other universities before opening to high school students, and eventually to anyone aged 13 and over.\r\n"
                                + "Facebook now allows any users who declare themselves to be at least 13 years old to become registered users of the site.\r\n",
                CompanyAddress = "1 Hacker Way, Menlo Park, CA, United States.\r\n",
                HiringProfiles = new List<HiringProfile> { FacebookHiringProfile } as ICollection<HiringProfile>
            };
          
            context.Companies.AddOrUpdate(
                t => t.CompanyName,
                FacebookIndia
            );
        }

        private void HiringProcessSeed(AuroraContext context) {
            var Facebook = context.HiringProfiles.Include("ProcessSteps").Where(t => t.Company.CompanyId == 1).FirstOrDefault();
            Facebook.ProcessSteps.Add(Written);
            Facebook.ProcessSteps.Add(TechnicalRound);
            Facebook.ProcessSteps.Add(TechnicalRound);
            Facebook.ProcessSteps.Add(HRRound);
        }

        private void StudentModelSeed(AuroraContext context)
        {
            var BLR = context.Campuses.Find(1);
            var CSE = context.Departments.Find(1);
            var USR = context.Roles.Find(5);

            var Vaibhav = new Student { 
                Campus = BLR,
                CGPA = 8.0f,
                Contact = "+919964005183",
                Department = CSE,
                Email = "vaibhavkrishna29@outlook.com",
                FirstName = "Vaibhav",
                LastName = "Krishna",
                ProfileLink = "https://www.facebook.com/vaibhav.krishna.31",
                RegistrationNumber = "BL.EN.U4.CSE12002",
                Role = USR,
                TenthPercentage = 88,
                TwelvthPercentage = 92,
                Username = "vaibhav.krishna",
                Company = null
            };

            context.Students.AddOrUpdate(
                t => t.Username,
                Vaibhav
            );
        }

        private void ProgressionModelSeed(AuroraContext context)
        {
            var Facebook = context.HiringProfiles.Find(1);
            var Vaibhav = context.Students.Find(4);
            var StudentProgress = new StudentProgression { 
                HiringProfile = Facebook,
                Student = Vaibhav,
                Total = Facebook.ProcessSteps.Count,
                Cleared = 1
            };

            context.StudentProgressions.AddOrUpdate(
                t => t.HiringId,
                StudentProgress
            );
        }
    }
}
