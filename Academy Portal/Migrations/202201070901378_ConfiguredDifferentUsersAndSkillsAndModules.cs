namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfiguredDifferentUsersAndSkillsAndModules : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Modules", "FacultyUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "SkillID", "dbo.Skills");
            DropIndex("dbo.Modules", new[] { "FacultyUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "SkillID" });
            AddColumn("dbo.AspNetUsers", "Skill_SkillID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Module_ModuleID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Skill_SkillID");
            CreateIndex("dbo.AspNetUsers", "Module_ModuleID");
            AddForeignKey("dbo.AspNetUsers", "Module_ModuleID", "dbo.Modules", "ModuleID");
            AddForeignKey("dbo.AspNetUsers", "Skill_SkillID", "dbo.Skills", "SkillID");
            DropColumn("dbo.Modules", "FacultyUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Modules", "FacultyUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "Skill_SkillID", "dbo.Skills");
            DropForeignKey("dbo.AspNetUsers", "Module_ModuleID", "dbo.Modules");
            DropIndex("dbo.AspNetUsers", new[] { "Module_ModuleID" });
            DropIndex("dbo.AspNetUsers", new[] { "Skill_SkillID" });
            DropColumn("dbo.AspNetUsers", "Module_ModuleID");
            DropColumn("dbo.AspNetUsers", "Skill_SkillID");
            CreateIndex("dbo.AspNetUsers", "SkillID");
            CreateIndex("dbo.Modules", "FacultyUser_Id");
            AddForeignKey("dbo.AspNetUsers", "SkillID", "dbo.Skills", "SkillID", cascadeDelete: true);
            AddForeignKey("dbo.Modules", "FacultyUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
