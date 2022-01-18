namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBatchIDToEmployeeUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BatchID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BatchID");
        }
    }
}
