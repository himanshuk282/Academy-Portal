namespace Academy_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateRolesTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Admin')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Faculty')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Employee')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
