namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserIDColumnInHelp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Helps", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Helps", "UserId");
        }
    }
}
