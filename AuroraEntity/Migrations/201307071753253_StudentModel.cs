namespace Aurora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Generates the Student Model on the database and fixes unique key constraint issues
    /// </summary>
    public partial class StudentModel : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE [dbo].[Companies] ADD CONSTRAINT [AK_Companies_CompanyName] UNIQUE NONCLUSTERED ([CompanyName] ASC)");
            Sql("ALTER TABLE [dbo].[HiringProfiles] ADD CONSTRAINT [AK_HiringProfiles_HiringProfileName] UNIQUE NONCLUSTERED ([HiringProfileName] ASC)");
            Sql("ALTER TABLE [dbo].[ProcessSteps] ADD CONSTRAINT [AK_ProcessSteps_ProcessStepName] UNIQUE NONCLUSTERED ([ProcessStepName] ASC)");

            CreateTable(
                "dbo.Students",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    CompanyId = c.Int(),
                    TenthPercentage = c.Single(nullable: false, defaultValue: 0),
                    TwelvthPercentage = c.Single(nullable: false, defaultValue: 0),
                    CGPA = c.Single(nullable: false, defaultValue: 0),
                    UnderGraduatePercentage = c.Single(nullable: false, defaultValue: 0),
                    StandingArrears = c.Int(nullable: false, defaultValue: 0),
                    HistoricalArrears = c.Int(nullable: false, defaultValue: 0),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true, name: "FK_Students_Users")
                .ForeignKey("dbo.Companies", t => t.CompanyId, name: "FK_Students_Companies");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Students", name: "FK_Students_Companies");
            DropForeignKey("dbo.Students", name: "FK_Students_Users");
            DropTable("dbo.Students");
            Sql("ALTER TABLE [dbo].[ProcessSteps] DROP CONSTRAINT [AK_ProcessSteps_ProcessStepName]");
            Sql("ALTER TABLE [dbo].[HiringProfiles] DROP CONSTRAINT [AK_HiringProfiles_HiringProfileName]");
            Sql("ALTER TABLE [dbo].[Companies] DROP CONSTRAINT [AK_Companies_CompanyName]");
        }
    }
}
