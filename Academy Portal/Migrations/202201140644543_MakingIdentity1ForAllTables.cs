namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakingIdentity1ForAllTables : DbMigration
    {
        public override void Up()
        {
            Sql("DBCC CHECKIDENT('Skills', RESEED, 0)");
            Sql("DBCC CHECKIDENT('Batches', RESEED, 0)");
            Sql("DBCC CHECKIDENT('Helps', RESEED, 0)");
            Sql("DBCC CHECKIDENT('Modules', RESEED, 0)");
        }
        
        public override void Down()
        {
        }
    }
}
