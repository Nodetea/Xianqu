namespace Xianqu.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUserRoles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetRoles", "Discriminator");
        }
    }
}
