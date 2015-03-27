namespace BankAccountDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_UserProfileAccounts : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BasicAccount", "UserProfileID");
            AddForeignKey("dbo.BasicAccount", "UserProfileID", "dbo.UserProfile", "UserProfileID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasicAccount", "UserProfileID", "dbo.UserProfile");
            DropIndex("dbo.BasicAccount", new[] { "UserProfileID" });
        }
    }
}
