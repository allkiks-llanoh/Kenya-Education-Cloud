namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPinToVoucherPins : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VoucherPins", "Pin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VoucherPins", "Pin");
        }
    }
}
