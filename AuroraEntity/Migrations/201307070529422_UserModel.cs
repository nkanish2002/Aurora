namespace Aurora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Generates the user model and role management on the database
    /// </summary>
    public partial class UserModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(),
                        DepartmentId = c.Int(),
                        CampusId = c.Int(),
                        Username = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 50),
                        RegistrationNumber = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                        Contact = c.String(nullable: false, maxLength: 50),
                        ProfileLink = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, name: "FK_Users_Roles")
                .ForeignKey("dbo.Departments", t => t.DepartmentId, name: "FK_Users_Departments")
                .ForeignKey("dbo.Campus", t => t.CampusId, name: "FK_Users_Campus")
                .Index(t => t.Username, unique: true)
                .Index(t => t.RegistrationNumber, unique: true)
                .Index(t => t.Email, unique: true);
        }

        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "RegistrationNumber" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropForeignKey("dbo.Users", name: "FK_Users_Campus");
            DropForeignKey("dbo.Users", name: "FK_Users_Departments");
            DropForeignKey("dbo.Users", name: "FK_Users_Roles");
            DropTable("dbo.Users");
        }
    }
}
