namespace BankAccountDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserProfileFirstName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "FirstName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "FirstName");
        }
    }
}
