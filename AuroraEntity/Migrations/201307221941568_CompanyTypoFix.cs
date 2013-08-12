namespace Aurora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyTypoFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HiringProfiles", name: "FK_HiringProfiles_Companies");
            DropForeignKey("dbo.Students", name: "FK_Students_Companies");
            DropPrimaryKey("dbo.Companies", new[] { "ComapnyId" });
            DropColumn("dbo.Companies", "ComapnyId");
            AddColumn("dbo.Companies", "CompanyId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Companies", "CompanyId");
            AddForeignKey("dbo.HiringProfiles", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true, name: "FK_HiringProfiles_Companies");
            AddForeignKey("dbo.Students", "CompanyId", "dbo.Companies", "CompanyId", name: "FK_Students_Companies");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", name: "FK_Students_Companies");
            DropForeignKey("dbo.HiringProfiles", name: "FK_HiringProfiles_Companies");
            DropPrimaryKey("dbo.Companies", new[] { "CompanyId" });
            DropColumn("dbo.Companies", "CompanyId");
            AddColumn("dbo.Companies", "ComapnyId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Companies", "ComapnyId");
            AddForeignKey("dbo.Students", "CompanyId", "dbo.Companies", "ComapnyId", name: "FK_Students_Companies");
            AddForeignKey("dbo.HiringProfiles", "CompanyId", "dbo.Companies", "ComapnyId", cascadeDelete: true, name: "FK_HiringProfiles_Companies");
        }
    }
}
