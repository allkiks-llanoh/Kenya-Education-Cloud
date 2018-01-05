namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSchoolAdminSchoolOneToOneOrZero : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoolAdmins", "SchoolAdmin_Id", "dbo.ShoolAdmins");
            DropIndex("dbo.ShoolAdmins", new[] { "SchoolAdmin_Id" });
            AddColumn("dbo.Schools", "SchoolAdmin_Id", c => c.Int());
            CreateIndex("dbo.Schools", "SchoolAdmin_Id");
            AddForeignKey("dbo.Schools", "SchoolAdmin_Id", "dbo.ShoolAdmins", "Id");
            DropColumn("dbo.ShoolAdmins", "SchoolAdmin_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoolAdmins", "SchoolAdmin_Id", c => c.Int());
            DropForeignKey("dbo.Schools", "SchoolAdmin_Id", "dbo.ShoolAdmins");
            DropIndex("dbo.Schools", new[] { "SchoolAdmin_Id" });
            DropColumn("dbo.Schools", "SchoolAdmin_Id");
            CreateIndex("dbo.ShoolAdmins", "SchoolAdmin_Id");
            AddForeignKey("dbo.ShoolAdmins", "SchoolAdmin_Id", "dbo.ShoolAdmins", "Id");
        }
    }
}
