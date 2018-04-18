namespace KEC.Curation.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubjectId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SubjectId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SubjectId");
        }
    }
}
