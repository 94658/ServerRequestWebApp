namespace ServerRequestWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDepartmentUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_UserProfile", "Department", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_UserProfile", "Department");
        }
    }
}
