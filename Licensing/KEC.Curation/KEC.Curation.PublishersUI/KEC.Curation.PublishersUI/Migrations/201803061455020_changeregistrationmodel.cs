namespace KEC.Curation.PublishersUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeregistrationmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Contact", c => c.String());
            AddColumn("dbo.AspNetUsers", "KraPin", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "PostalAddress", c => c.String());
            AddColumn("dbo.AspNetUsers", "BusinessNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BusinessNumber");
            DropColumn("dbo.AspNetUsers", "PostalAddress");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "KraPin");
            DropColumn("dbo.AspNetUsers", "Contact");
        }
    }
}
