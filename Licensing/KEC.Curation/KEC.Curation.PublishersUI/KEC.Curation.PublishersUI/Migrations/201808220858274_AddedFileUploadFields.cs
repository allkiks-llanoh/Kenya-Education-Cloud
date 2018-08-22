namespace KEC.Curation.PublishersUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileUploadFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegistrationFilesavedName", c => c.String());
            AddColumn("dbo.AspNetUsers", "KraPinFilesavedName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "KraPinFilesavedName");
            DropColumn("dbo.AspNetUsers", "RegistrationFilesavedName");
        }
    }
}
