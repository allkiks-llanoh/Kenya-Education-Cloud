namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoucherStatustMap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vouchers", "Status_Id", c => c.Int());
            CreateIndex("dbo.Vouchers", "Status_Id");
            AddForeignKey("dbo.Vouchers", "Status_Id", "dbo.Statuses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vouchers", "Status_Id", "dbo.Statuses");
            DropIndex("dbo.Vouchers", new[] { "Status_Id" });
            DropColumn("dbo.Vouchers", "Status_Id");
        }
    }
}
