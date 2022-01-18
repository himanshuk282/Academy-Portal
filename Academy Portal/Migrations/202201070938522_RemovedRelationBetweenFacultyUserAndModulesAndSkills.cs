namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRelationBetweenFacultyUserAndModulesAndSkills : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Skill_SkillID", "dbo.Skills");
            DropForeignKey("dbo.AspNetUsers", "Module_ModuleID", "dbo.Modules");
            DropIndex("dbo.AspNetUsers", new[] { "Skill_SkillID" });
            DropIndex("dbo.AspNetUsers", new[] { "Module_ModuleID" });
            DropColumn("dbo.AspNetUsers", "Skill_SkillID");
            DropColumn("dbo.AspNetUsers", "Module_ModuleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Module_ModuleID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Skill_SkillID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Module_ModuleID");
            CreateIndex("dbo.AspNetUsers", "Skill_SkillID");
            AddForeignKey("dbo.AspNetUsers", "Module_ModuleID", "dbo.Modules", "ModuleID");
            AddForeignKey("dbo.AspNetUsers", "Skill_SkillID", "dbo.Skills", "SkillID");
        }
    }
}
