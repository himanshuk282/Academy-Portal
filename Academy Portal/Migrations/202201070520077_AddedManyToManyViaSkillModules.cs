namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedManyToManyViaSkillModules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkillModules",
                c => new
                    {
                        SkillID = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillID, t.ModuleID })
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillID, cascadeDelete: true)
                .Index(t => t.SkillID)
                .Index(t => t.ModuleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillModules", "SkillID", "dbo.Skills");
            DropForeignKey("dbo.SkillModules", "ModuleID", "dbo.Modules");
            DropIndex("dbo.SkillModules", new[] { "ModuleID" });
            DropIndex("dbo.SkillModules", new[] { "SkillID" });
            DropTable("dbo.SkillModules");
        }
    }
}
