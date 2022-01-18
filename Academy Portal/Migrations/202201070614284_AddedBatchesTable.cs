namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBatchesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        BatchID = c.Int(nullable: false, identity: true),
                        Technology = c.String(nullable: false),
                        BatchStartDate = c.DateTime(nullable: false),
                        BatchEndDate = c.DateTime(nullable: false),
                        BatchCapacity = c.Int(nullable: false),
                        ClassroomName = c.String(nullable: false, maxLength: 100),
                        ModuleID = c.Int(nullable: false),
                        SkillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BatchID)
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillID, cascadeDelete: true)
                .Index(t => t.ModuleID)
                .Index(t => t.SkillID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Batches", "SkillID", "dbo.Skills");
            DropForeignKey("dbo.Batches", "ModuleID", "dbo.Modules");
            DropIndex("dbo.Batches", new[] { "SkillID" });
            DropIndex("dbo.Batches", new[] { "ModuleID" });
            DropTable("dbo.Batches");
        }
    }
}
