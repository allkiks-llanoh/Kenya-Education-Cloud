namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNameFromStatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vouchers", "StatusId");
            DropColumn("dbo.Statuses", "StatusName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Statuses", "StatusName", c => c.String());
            AddColumn("dbo.Vouchers", "StatusId", c => c.Int(nullable: false));
        }
    }
}
