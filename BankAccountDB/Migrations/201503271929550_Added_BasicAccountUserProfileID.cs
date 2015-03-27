namespace BankAccountDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_BasicAccountUserProfileID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasicAccount", "UserProfileID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasicAccount", "UserProfileID");
        }
    }
}
