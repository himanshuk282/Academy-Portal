namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinorChangesInHelpTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Helps", "DateOfTicket", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Helps", "DateOfTicket", c => c.DateTime());
        }
    }
}
