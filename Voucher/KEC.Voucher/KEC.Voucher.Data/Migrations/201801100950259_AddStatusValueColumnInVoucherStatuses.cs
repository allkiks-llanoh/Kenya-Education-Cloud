namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusValueColumnInVoucherStatuses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statuses", "StatusValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Statuses", "StatusValue");
        }
    }
}
