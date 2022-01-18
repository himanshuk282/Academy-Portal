namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredAttributeFromDAteInHelp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Helps", "DateOfTicket", c => c.DateTime(defaultValueSql:"GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Helps", "DateOfTicket", c => c.DateTime(nullable: false));
        }
    }
}
