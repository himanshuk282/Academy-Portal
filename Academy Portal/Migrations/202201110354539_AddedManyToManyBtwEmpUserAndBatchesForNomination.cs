namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedManyToManyBtwEmpUserAndBatchesForNomination : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeUserBatches",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        BatchID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.BatchID })
                .ForeignKey("dbo.Batches", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.BatchID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.BatchID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeUserBatches", "BatchID", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmployeeUserBatches", "EmployeeID", "dbo.Batches");
            DropIndex("dbo.EmployeeUserBatches", new[] { "BatchID" });
            DropIndex("dbo.EmployeeUserBatches", new[] { "EmployeeID" });
            DropTable("dbo.EmployeeUserBatches");
        }
    }
}
