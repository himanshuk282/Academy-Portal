namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRemainingColumnsToUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserId");
        }
    }
}
