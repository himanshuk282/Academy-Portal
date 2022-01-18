namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHelpTableToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Helps",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        Issue = c.String(),
                        Description = c.String(),
                        ResolutionProvided = c.String(),
                        DateOfTicket = c.DateTime(),
                    })
                .PrimaryKey(t => t.RequestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Helps");
        }
    }
}
