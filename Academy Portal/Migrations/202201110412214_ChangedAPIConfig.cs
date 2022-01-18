namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAPIConfig : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.EmployeeUserBatches", name: "EmployeeID", newName: "Batch_BatchID");
            RenameColumn(table: "dbo.EmployeeUserBatches", name: "BatchID", newName: "EmployeeUser_Id");
            RenameIndex(table: "dbo.EmployeeUserBatches", name: "IX_BatchID", newName: "IX_EmployeeUser_Id");
            RenameIndex(table: "dbo.EmployeeUserBatches", name: "IX_EmployeeID", newName: "IX_Batch_BatchID");
            DropPrimaryKey("dbo.EmployeeUserBatches");
            AddPrimaryKey("dbo.EmployeeUserBatches", new[] { "EmployeeUser_Id", "Batch_BatchID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.EmployeeUserBatches");
            AddPrimaryKey("dbo.EmployeeUserBatches", new[] { "EmployeeID", "BatchID" });
            RenameIndex(table: "dbo.EmployeeUserBatches", name: "IX_Batch_BatchID", newName: "IX_EmployeeID");
            RenameIndex(table: "dbo.EmployeeUserBatches", name: "IX_EmployeeUser_Id", newName: "IX_BatchID");
            RenameColumn(table: "dbo.EmployeeUserBatches", name: "EmployeeUser_Id", newName: "BatchID");
            RenameColumn(table: "dbo.EmployeeUserBatches", name: "Batch_BatchID", newName: "EmployeeID");
        }
    }
}
