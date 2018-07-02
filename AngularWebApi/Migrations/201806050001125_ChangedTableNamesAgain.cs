namespace AngularWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTableNamesAgain : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUserLogins", newName: "UserLogin");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserLogin", newName: "AspNetUserLogins");
        }
    }
}
