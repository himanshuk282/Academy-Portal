namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSecurityQuestionsToAll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SecurityParameter1", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "SecurityParameter2", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "SecurityParameter3", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SecurityParameter3");
            DropColumn("dbo.AspNetUsers", "SecurityParameter2");
            DropColumn("dbo.AspNetUsers", "SecurityParameter1");
        }
    }
}
