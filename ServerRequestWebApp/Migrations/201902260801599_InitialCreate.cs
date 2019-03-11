namespace ServerRequestWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Department",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tb_Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.tb_UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.tb_Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.tb_User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.tb_ServerAccess",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        target_server = c.String(),
                        period = c.Int(nullable: false),
                        notes = c.String(),
                        created_by = c.String(),
                        created_on = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        supervised_by = c.String(),
                        supervised_on = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        approved_by = c.String(),
                        approved_on = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tb_UserProfile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Department = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tb_User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.tb_UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tb_UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.tb_User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_UserRole", "UserId", "dbo.tb_User");
            DropForeignKey("dbo.tb_UserLogin", "UserId", "dbo.tb_User");
            DropForeignKey("dbo.tb_UserClaim", "UserId", "dbo.tb_User");
            DropForeignKey("dbo.tb_UserRole", "RoleId", "dbo.tb_Role");
            DropIndex("dbo.tb_UserLogin", new[] { "UserId" });
            DropIndex("dbo.tb_UserClaim", new[] { "UserId" });
            DropIndex("dbo.tb_User", "UserNameIndex");
            DropIndex("dbo.tb_UserRole", new[] { "RoleId" });
            DropIndex("dbo.tb_UserRole", new[] { "UserId" });
            DropIndex("dbo.tb_Role", "RoleNameIndex");
            DropTable("dbo.tb_UserLogin");
            DropTable("dbo.tb_UserClaim");
            DropTable("dbo.tb_User");
            DropTable("dbo.tb_UserProfile");
            DropTable("dbo.tb_ServerAccess");
            DropTable("dbo.tb_UserRole");
            DropTable("dbo.tb_Role");
            DropTable("dbo.tb_Department");
        }
    }
}
