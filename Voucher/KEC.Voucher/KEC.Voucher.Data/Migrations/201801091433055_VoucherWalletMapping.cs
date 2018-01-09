namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoucherWalletMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vouchers", "Wallet_Id", c => c.Int());
            CreateIndex("dbo.Vouchers", "Wallet_Id");
            AddForeignKey("dbo.Vouchers", "Wallet_Id", "dbo.Wallets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vouchers", "Wallet_Id", "dbo.Wallets");
            DropIndex("dbo.Vouchers", new[] { "Wallet_Id" });
            DropColumn("dbo.Vouchers", "Wallet_Id");
        }
    }
}
