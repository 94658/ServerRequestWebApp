namespace ServerRequestWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelateDeptUsers : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_Department", "ID");
            DropColumn("dbo.tb_UserProfile", "ID");
            DropColumn("dbo.tb_UserProfile", "Department");
            //DropPrimaryKey("dbo.tb_Department");
            //DropPrimaryKey("dbo.tb_UserProfile");
            AddColumn("dbo.tb_Department", "DepartmentId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.tb_UserProfile", "UserProfileId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.tb_UserProfile", "DepartmentId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tb_Department", "DepartmentId");
            AddPrimaryKey("dbo.tb_UserProfile", "UserProfileId");
            CreateIndex("dbo.tb_UserProfile", "DepartmentId");
            AddForeignKey("dbo.tb_UserProfile", "DepartmentId", "dbo.tb_Department", "DepartmentId", cascadeDelete: true);
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_UserProfile", "Department", c => c.String(nullable: false));
            AddColumn("dbo.tb_UserProfile", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.tb_Department", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.tb_UserProfile", "DepartmentId", "dbo.tb_Department");
            DropIndex("dbo.tb_UserProfile", new[] { "DepartmentId" });
            DropPrimaryKey("dbo.tb_UserProfile");
            DropPrimaryKey("dbo.tb_Department");
            DropColumn("dbo.tb_UserProfile", "DepartmentId");
            DropColumn("dbo.tb_UserProfile", "UserProfileId");
            DropColumn("dbo.tb_Department", "DepartmentId");
            AddPrimaryKey("dbo.tb_UserProfile", "ID");
            AddPrimaryKey("dbo.tb_Department", "ID");
        }
    }
}
