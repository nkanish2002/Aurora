namespace Aurora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// Generates all the initial primitives required for the basic functionality on the database
    /// </summary>
    public partial class InitialPrimitives : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campus",
                c => new
                    {
                        CampusId = c.Int(nullable: false, identity: true),
                        CampusName = c.String(nullable: false, maxLength: 11),
                    })
                .PrimaryKey(t => t.CampusId);
            Sql("ALTER TABLE [dbo].[Campus] ADD CONSTRAINT [AK_Campus_CampusName] UNIQUE NONCLUSTERED ([CampusName] ASC)");
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 50),
                        IsUndergraduate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            Sql("ALTER TABLE [dbo].[Departments] ADD CONSTRAINT [AK_Departments_DepartmentName] UNIQUE NONCLUSTERED ([DepartmentName] ASC)");
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.RoleId);
            Sql("ALTER TABLE [dbo].[Roles] ADD CONSTRAINT [AK_Roles_RoleName] UNIQUE NONCLUSTERED ([RoleName] ASC)");
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ResourceId = c.Int(nullable: false, identity: true),
                        ResourceName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ResourceId);
            Sql("ALTER TABLE [dbo].[Resources] ADD CONSTRAINT [AK_Resources_ResourceName] UNIQUE NONCLUSTERED ([ResourceName] ASC)");
        }
        
        public override void Down()
        {
            Sql("ALTER TABLE [dbo].[Resources] DROP CONSTRAINT [AK_Resources_ResourceName]");
            Sql("ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [AK_Roles_RoleName]");
            Sql("ALTER TABLE [dbo].[Departments] DROP CONSTRAINT [AK_Departments_DepartmentName]");
            Sql("ALTER TABLE [dbo].[Campus] DROP CONSTRAINT [AK_Campus_CampusName]");
            DropTable("dbo.Resources");
            DropTable("dbo.Roles");
            DropTable("dbo.Departments");
            DropTable("dbo.Campus");
        }
    }
}
