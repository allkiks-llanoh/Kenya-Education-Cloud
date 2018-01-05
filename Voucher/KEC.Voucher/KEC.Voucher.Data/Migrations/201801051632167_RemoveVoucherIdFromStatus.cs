namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveVoucherIdFromStatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Statuses", "VoucherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Statuses", "VoucherId", c => c.Int(nullable: false));
        }
    }
}
