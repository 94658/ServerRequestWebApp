namespace ServerRequestWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_UserProfile", "DepartmentId", "dbo.tb_Department");
            DropIndex("dbo.tb_UserProfile", new[] { "DepartmentId" });
            DropColumn("dbo.tb_UserProfile", "DepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_UserProfile", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_UserProfile", "DepartmentId");
            AddForeignKey("dbo.tb_UserProfile", "DepartmentId", "dbo.tb_Department", "DepartmentId", cascadeDelete: true);
        }
    }
}
