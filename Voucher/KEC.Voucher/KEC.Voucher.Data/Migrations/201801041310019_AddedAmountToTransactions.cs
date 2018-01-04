namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAmountToTransactions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Amount");
        }
    }
}
