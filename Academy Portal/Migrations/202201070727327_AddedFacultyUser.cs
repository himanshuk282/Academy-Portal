namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFacultyUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modules", "FacultyUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "SkillID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Modules", "FacultyUser_Id");
            AddForeignKey("dbo.Modules", "FacultyUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "FacultyUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Modules", new[] { "FacultyUser_Id" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "SkillID");
            DropColumn("dbo.Modules", "FacultyUser_Id");
        }
    }
}
