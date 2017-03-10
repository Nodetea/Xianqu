namespace Xianqu.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCloumnSexAndBirthDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserSex", c => c.String(maxLength: 10));
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "UserSex");
        }
    }
}
