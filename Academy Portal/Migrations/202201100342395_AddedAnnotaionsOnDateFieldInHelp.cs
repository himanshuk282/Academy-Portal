namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnnotaionsOnDateFieldInHelp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Helps", "DateOfTicket", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Helps", "DateOfTicket", c => c.DateTime());
        }
    }
}
