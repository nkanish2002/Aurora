namespace Aurora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentProgressions",
                c => new
                    {
                        HiringId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Cleared = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HiringId, t.StudentId })
                .ForeignKey("dbo.HiringProfiles", t => t.HiringId, cascadeDelete: true, name: "FK_StudentProgressions_HiringProfiles")
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true, name: "FK_StudentProgressions_Students");          
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentProgressions", name: "FK_StudentProgressions_Students");
            DropForeignKey("dbo.StudentProgressions", name: "FK_StudentProgressions_HiringProfiles");
            DropTable("dbo.StudentProgressions");
        }
    }
}
