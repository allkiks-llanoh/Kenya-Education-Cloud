namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMigrationVoucherWalletMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wallets", "Voucher_Id", c => c.Int());
            CreateIndex("dbo.Wallets", "Voucher_Id");
            AddForeignKey("dbo.Wallets", "Voucher_Id", "dbo.Vouchers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallets", "Voucher_Id", "dbo.Vouchers");
            DropIndex("dbo.Wallets", new[] { "Voucher_Id" });
            DropColumn("dbo.Wallets", "Voucher_Id");
        }
    }
}
