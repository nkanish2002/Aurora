namespace Aurora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// Generates the permission model system on the database
    /// </summary>
    public partial class PermissionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        ResourceId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CanCreate = c.Boolean(nullable: false),
                        CanRead = c.Boolean(nullable: false),
                        CanUpdate = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false)
                    })
                .PrimaryKey(t => t.PermissionId)
                .ForeignKey("dbo.Resources", t => t.ResourceId, cascadeDelete: true, name: "FK_Permissions_Resources")
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true, name: "FK_Permissions_Roles")
                .Index(t => t.ResourceId)
                .Index(t => t.RoleId);
            Sql("ALTER TABLE [dbo].[Permissions] ADD CONSTRAINT [AK_Permissions_ResourceRole] UNIQUE NONCLUSTERED ([ResourceId] ASC, [RoleId] ASC)");
        }
        
        public override void Down()
        {
            Sql("ALTER TABLE [dbo].[Permissions] DROP CONSTRAINT [AK_Permissions_ResourceRole]");
            DropIndex("dbo.Permissions", new[] { "RoleId" });
            DropIndex("dbo.Permissions", new[] { "ResourceId" });
            DropForeignKey("dbo.Permissions", "FK_Permissions_Resources");
            DropForeignKey("dbo.Permissions", "FK_Permissions_Roles");
            DropTable("dbo.Permissions");
        }
    }
}
