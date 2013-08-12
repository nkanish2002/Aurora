namespace Aurora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Generates the Company and Hiring Profile models on the database
    /// </summary>
    public partial class CompanyModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                {
                    ComapnyId = c.Int(nullable: false, identity: true),
                    CompanyName = c.String(nullable: false, maxLength: 50),
                    CompanyDescription = c.String(nullable: false),
                    CompanyAddress = c.String(nullable: false, maxLength: 300),
                    CompanyUrl = c.String(nullable: false, maxLength: 100),
                    CompanyProfile = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.ComapnyId);

            CreateTable(
                "dbo.HiringProfiles",
                c => new
                {
                    HiringProfileId = c.Int(nullable: false, identity: true),
                    CompanyId = c.Int(nullable: false),
                    CampusId = c.Int(nullable: false),
                    HiringProfileName = c.String(nullable: false, maxLength: 50),
                    Batch = c.Int(nullable: false),
                    TenthPercentage = c.Single(nullable: false, defaultValue: 0),
                    TwelvthPercentage = c.Single(nullable: false, defaultValue: 0),
                    CGPA = c.Single(nullable: false, defaultValue: 0),
                    UnderGraduatePercentage = c.Single(nullable: false, defaultValue: 0),
                    StandingArrears = c.Int(nullable: false, defaultValue: 0),
                    HistoricalArrears = c.Int(nullable: false, defaultValue: 0),
                    JobProfile = c.String(nullable: false, maxLength: 50),
                    UnderGraduateCTC = c.Single(nullable: false, defaultValue: 0),
                    PostGraduateCTC = c.Single(nullable: false, defaultValue: 0),
                    InternshipStipend = c.Single(nullable: false, defaultValue: 0),
                    HiringDate = c.DateTime(nullable: false),
                    Comments = c.String(maxLength: 300),
                })
                .PrimaryKey(t => t.HiringProfileId)
                .ForeignKey("dbo.Campus", t => t.CampusId, cascadeDelete: true, name: "FK_HiringProfiles_Campus")
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true, name: "FK_HiringProfiles_Companies");

            CreateTable(
                "dbo.HiringFrom",
                c => new
                {
                    DepartmentId = c.Int(nullable: false),
                    HiringProfileId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.DepartmentId, t.HiringProfileId })
                .ForeignKey("dbo.HiringProfiles", t => t.HiringProfileId, cascadeDelete: true, name: "FK_HiringFrom_HiringProfiles")
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true, name: "FK_HiringFrom_Departments");
        }

        public override void Down()
        {
            DropForeignKey("dbo.HiringFrom", name: "FK_HiringFrom_Departments");
            DropForeignKey("dbo.HiringFrom", name: "FK_HiringFrom_HiringProfiles");
            DropForeignKey("dbo.HiringProfiles", name: "FK_HiringProfiles_Companies");
            DropForeignKey("dbo.HiringProfiles", name: "FK_HiringProfiles_Campus");
            DropTable("dbo.HiringFrom");
            DropTable("dbo.HiringProfiles");
            DropTable("dbo.Companies");
        }
    }
}