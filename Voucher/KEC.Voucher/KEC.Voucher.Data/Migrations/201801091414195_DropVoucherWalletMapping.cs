namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropVoucherWalletMapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wallets", "Id", "dbo.Vouchers");
            DropIndex("dbo.Wallets", new[] { "Id" });
            DropPrimaryKey("dbo.Wallets");
            AlterColumn("dbo.Wallets", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Wallets", "Id");
            DropColumn("dbo.Vouchers", "WalletId");
            DropColumn("dbo.Wallets", "VoucherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wallets", "VoucherId", c => c.Int(nullable: false));
            AddColumn("dbo.Vouchers", "WalletId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Wallets");
            AlterColumn("dbo.Wallets", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Wallets", "Id");
            CreateIndex("dbo.Wallets", "Id");
            AddForeignKey("dbo.Wallets", "Id", "dbo.Vouchers", "Id");
        }
    }
}
