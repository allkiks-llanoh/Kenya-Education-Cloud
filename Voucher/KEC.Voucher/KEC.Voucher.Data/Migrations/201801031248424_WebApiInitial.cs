namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebApiInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountyId = c.Int(nullable: false),
                        BatchNumber = c.String(),
                        SerialNumber = c.String(),
                        Year = c.Int(nullable: false),
                        SchoolTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counties", t => t.CountyId)
                .ForeignKey("dbo.SchoolTypes", t => t.SchoolTypeId, cascadeDelete: true)
                .Index(t => t.CountyId)
                .Index(t => t.SchoolTypeId);
            
            CreateTable(
                "dbo.Counties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountyName = c.String(),
                        CountyCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        SchoolCode = c.String(),
                        SchoolTypeId = c.Int(nullable: false),
                        CountyId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateChanged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolTypes", t => t.SchoolTypeId)
                .ForeignKey("dbo.Counties", t => t.CountyId)
                .Index(t => t.SchoolTypeId)
                .Index(t => t.CountyId);
            
            CreateTable(
                "dbo.FundAllocations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SchoolId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.SchoolId);
            
            CreateTable(
                "dbo.SchoolTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherCode = c.String(),
                        VoucherSerial = c.String(),
                        VoucherYear = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        WalletId = c.Int(nullable: false),
                        BatchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.Batches", t => t.BatchId)
                .Index(t => t.SchoolId)
                .Index(t => t.BatchId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherId = c.Int(nullable: false),
                        TransactionDescription = c.String(),
                        PinId = c.Int(nullable: false),
                        SchoolAdminId = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoolAdmins", t => t.SchoolAdminId, cascadeDelete: true)
                .ForeignKey("dbo.Vouchers", t => t.VoucherId)
                .Index(t => t.VoucherId)
                .Index(t => t.SchoolAdminId);
            
            CreateTable(
                "dbo.ShoolAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        guid = c.String(),
                        SchoolAdmin_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoolAdmins", t => t.SchoolAdmin_Id)
                .Index(t => t.SchoolAdmin_Id);
            
            CreateTable(
                "dbo.VoucherPins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherId = c.Int(nullable: false),
                        Status = c.String(),
                        CreatedOnUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vouchers", t => t.VoucherId)
                .Index(t => t.VoucherId);
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                        StatusDescription = c.String(),
                        VoucherId = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vouchers", t => t.VoucherId)
                .Index(t => t.VoucherId);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        VoucherId = c.Int(nullable: false),
                        WalletAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpdatedOnUtc = c.DateTime(nullable: false),
                        SchoolID_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolID_Id)
                .ForeignKey("dbo.Vouchers", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.SchoolID_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderGuid = c.String(),
                        StorId = c.String(),
                        CustomerId = c.String(),
                        OrderNumber = c.String(),
                        OrderTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vouchers", "BatchId", "dbo.Batches");
            DropForeignKey("dbo.Batches", "SchoolTypeId", "dbo.SchoolTypes");
            DropForeignKey("dbo.Schools", "CountyId", "dbo.Counties");
            DropForeignKey("dbo.Vouchers", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Wallets", "Id", "dbo.Vouchers");
            DropForeignKey("dbo.Wallets", "SchoolID_Id", "dbo.Schools");
            DropForeignKey("dbo.Statuses", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.VoucherPins", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.Transactions", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.Transactions", "SchoolAdminId", "dbo.ShoolAdmins");
            DropForeignKey("dbo.ShoolAdmins", "SchoolAdmin_Id", "dbo.ShoolAdmins");
            DropForeignKey("dbo.Schools", "SchoolTypeId", "dbo.SchoolTypes");
            DropForeignKey("dbo.FundAllocations", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Batches", "CountyId", "dbo.Counties");
            DropIndex("dbo.Wallets", new[] { "SchoolID_Id" });
            DropIndex("dbo.Wallets", new[] { "Id" });
            DropIndex("dbo.Statuses", new[] { "VoucherId" });
            DropIndex("dbo.VoucherPins", new[] { "VoucherId" });
            DropIndex("dbo.ShoolAdmins", new[] { "SchoolAdmin_Id" });
            DropIndex("dbo.Transactions", new[] { "SchoolAdminId" });
            DropIndex("dbo.Transactions", new[] { "VoucherId" });
            DropIndex("dbo.Vouchers", new[] { "BatchId" });
            DropIndex("dbo.Vouchers", new[] { "SchoolId" });
            DropIndex("dbo.FundAllocations", new[] { "SchoolId" });
            DropIndex("dbo.Schools", new[] { "CountyId" });
            DropIndex("dbo.Schools", new[] { "SchoolTypeId" });
            DropIndex("dbo.Batches", new[] { "SchoolTypeId" });
            DropIndex("dbo.Batches", new[] { "CountyId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Wallets");
            DropTable("dbo.Statuses");
            DropTable("dbo.VoucherPins");
            DropTable("dbo.ShoolAdmins");
            DropTable("dbo.Transactions");
            DropTable("dbo.Vouchers");
            DropTable("dbo.SchoolTypes");
            DropTable("dbo.FundAllocations");
            DropTable("dbo.Schools");
            DropTable("dbo.Counties");
            DropTable("dbo.Batches");
        }
    }
}
