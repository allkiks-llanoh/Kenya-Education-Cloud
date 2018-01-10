namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropStatustDescriptionColumnInVoucherStatuses : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Statuses", "StatusDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Statuses", "StatusDescription", c => c.String());
        }
    }
}
