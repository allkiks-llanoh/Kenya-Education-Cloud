namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropVoucherStatusRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Statuses", "VoucherId", "dbo.Vouchers");
            DropIndex("dbo.Statuses", new[] { "VoucherId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Statuses", "VoucherId");
            AddForeignKey("dbo.Statuses", "VoucherId", "dbo.Vouchers", "Id");
        }
    }
}
