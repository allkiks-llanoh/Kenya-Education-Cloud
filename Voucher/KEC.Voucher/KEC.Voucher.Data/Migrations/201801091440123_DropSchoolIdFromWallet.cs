namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropSchoolIdFromWallet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wallets", "SchoolID_Id", "dbo.Schools");
            DropIndex("dbo.Wallets", new[] { "SchoolID_Id" });
            DropColumn("dbo.Wallets", "SchoolID_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wallets", "SchoolID_Id", c => c.Int());
            CreateIndex("dbo.Wallets", "SchoolID_Id");
            AddForeignKey("dbo.Wallets", "SchoolID_Id", "dbo.Schools", "Id");
        }
    }
}
