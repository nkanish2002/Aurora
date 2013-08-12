namespace Aurora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Generates the Process Steps and Hiring Process models on the database
    /// </summary>
    public partial class HiringProcessModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcessSteps",
                c => new
                    {
                        ProcessStepId = c.Int(nullable: false, identity: true),
                        ProcessStepName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ProcessStepId);

            CreateTable(
                "dbo.HiringProcess",
                c => new
                    {
                        ConfigurationId = c.Int(nullable: false, identity: true), 
                        HiringProfileId = c.Int(nullable: false),
                        ProcessStepId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConfigurationId)
                .ForeignKey("dbo.HiringProfiles", t => t.HiringProfileId, cascadeDelete: true, name: "FK_HiringProcess_HiringProfiles")
                .ForeignKey("dbo.ProcessSteps", t => t.ProcessStepId, cascadeDelete: true, name: "FK_HiringProcess_ProcessSteps");          
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HiringProcess", name: "FK_HiringProcess_ProcessSteps");
            DropForeignKey("dbo.HiringProcess", name: "FK_HiringProcess_HiringProfiles");
            DropTable("dbo.HiringProcess");
            DropTable("dbo.ProcessSteps");
        }
    }
}
