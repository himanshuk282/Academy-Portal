namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBatchNominationStatusInEmployeeUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BatchNominationStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BatchNominationStatus");
        }
    }
}
