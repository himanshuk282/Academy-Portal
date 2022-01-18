namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ModuleID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "SkillID");
            AddForeignKey("dbo.AspNetUsers", "SkillID", "dbo.Skills", "SkillID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "SkillID", "dbo.Skills");
            DropIndex("dbo.AspNetUsers", new[] { "SkillID" });
            DropColumn("dbo.AspNetUsers", "ModuleID");
        }
    }
}
