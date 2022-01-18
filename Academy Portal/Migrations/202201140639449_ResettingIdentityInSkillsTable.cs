namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResettingIdentityInSkillsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DBCC CHECKIDENT ('Skills', RESEED, 1);");
        }
        
        public override void Down()
        {
        }
    }
}
