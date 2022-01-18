namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProfLevelAndTeachingHoursToFacultyUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Proficiencylevel", c => c.String());
            AddColumn("dbo.AspNetUsers", "TotalHoursOfTeaching", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TotalHoursOfTeaching");
            DropColumn("dbo.AspNetUsers", "Proficiencylevel");
        }
    }
}
