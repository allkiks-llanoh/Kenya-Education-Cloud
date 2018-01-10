namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivatedByColumnInVoucherStatuses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statuses", "ActivatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Statuses", "ActivatedBy");
        }
    }
}
