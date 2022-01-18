namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedResolutionStatusToHelp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Helps", "ResolutionStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Helps", "ResolutionStatus");
        }
    }
}
