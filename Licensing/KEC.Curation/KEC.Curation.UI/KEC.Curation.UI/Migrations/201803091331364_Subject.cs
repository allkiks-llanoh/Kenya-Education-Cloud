namespace KEC.Curation.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Subject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Subject");
        }
    }
}
