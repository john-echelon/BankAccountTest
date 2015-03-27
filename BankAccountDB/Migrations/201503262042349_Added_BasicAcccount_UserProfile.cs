namespace BankAccountDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_BasicAcccount_UserProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasicAccount",
                c => new
                    {
                        BasicAccountID = c.Int(nullable: false, identity: true),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InterestRate = c.Double(nullable: false),
                        StatusOfAccount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BasicAccountID);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserProfileID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserProfileID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfile");
            DropTable("dbo.BasicAccount");
        }
    }
}
