namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFacultyIdToBatchesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Batches", "FacultyID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Batches", "FacultyID");
        }
    }
}
