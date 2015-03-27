namespace BankAccountDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated_BasicAcccount_AddedProperty_AccountType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasicAccount", "AccountType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasicAccount", "AccountType");
        }
    }
}
