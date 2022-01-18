namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDataAnnotationsToSkillNameInSkills : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Skills", "SkillName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skills", "SkillName", c => c.String());
        }
    }
}
