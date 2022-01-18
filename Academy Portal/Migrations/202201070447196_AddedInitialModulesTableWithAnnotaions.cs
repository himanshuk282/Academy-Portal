namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInitialModulesTableWithAnnotaions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ModuleID = c.Int(nullable: false, identity: true),
                        Technology = c.String(nullable: false),
                        ProficiencyLevel = c.String(nullable: false, maxLength: 50),
                        ExecutionType = c.String(nullable: false),
                        CertificationType = c.String(nullable: false),
                        CertificationName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ModuleID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Modules");
        }
    }
}
