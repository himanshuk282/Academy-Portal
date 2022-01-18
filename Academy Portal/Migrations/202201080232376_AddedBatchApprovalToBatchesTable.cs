namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBatchApprovalToBatchesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Batches", "BatchApproval", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Batches", "BatchApproval");
        }
    }
}
