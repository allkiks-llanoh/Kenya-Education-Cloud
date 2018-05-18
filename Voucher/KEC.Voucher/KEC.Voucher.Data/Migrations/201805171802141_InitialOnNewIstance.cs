namespace KEC.Voucher.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialOnNewIstance : DbMigration
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
                        SchoolAdmin_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoolAdmins", t => t.SchoolAdmin_Id)
                .ForeignKey("dbo.SchoolTypes", t => t.SchoolTypeId)
                .ForeignKey("dbo.Counties", t => t.CountyId)
                .Index(t => t.SchoolTypeId)
                .Index(t => t.CountyId)
                .Index(t => t.SchoolAdmin_Id);
            
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
                "dbo.ShoolAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        guid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        BatchId = c.Int(nullable: false),
                        Status_Id = c.Int(),
                        Wallet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Statuses", t => t.Status_Id)
                .ForeignKey("dbo.Wallets", t => t.Wallet_Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.Batches", t => t.BatchId)
                .Index(t => t.SchoolId)
                .Index(t => t.BatchId)
                .Index(t => t.Status_Id)
                .Index(t => t.Wallet_Id);
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusValue = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        ActivatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherId = c.Int(nullable: false),
                        TransactionDescription = c.String(),
                        PinId = c.Int(nullable: false),
                        SchoolAdminId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOnUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoolAdmins", t => t.SchoolAdminId, cascadeDelete: true)
                .ForeignKey("dbo.Vouchers", t => t.VoucherId)
                .Index(t => t.VoucherId)
                .Index(t => t.SchoolAdminId);
            
            CreateTable(
                "dbo.VoucherPins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherId = c.Int(nullable: false),
                        Pin = c.String(),
                        Status = c.String(),
                        CreatedOnUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vouchers", t => t.VoucherId)
                .Index(t => t.VoucherId);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WalletAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpdatedOnUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Vouchers", "Wallet_Id", "dbo.Wallets");
            DropForeignKey("dbo.VoucherPins", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.Transactions", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.Transactions", "SchoolAdminId", "dbo.ShoolAdmins");
            DropForeignKey("dbo.Vouchers", "Status_Id", "dbo.Statuses");
            DropForeignKey("dbo.Schools", "SchoolTypeId", "dbo.SchoolTypes");
            DropForeignKey("dbo.Schools", "SchoolAdmin_Id", "dbo.ShoolAdmins");
            DropForeignKey("dbo.FundAllocations", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Batches", "CountyId", "dbo.Counties");
            DropIndex("dbo.VoucherPins", new[] { "VoucherId" });
            DropIndex("dbo.Transactions", new[] { "SchoolAdminId" });
            DropIndex("dbo.Transactions", new[] { "VoucherId" });
            DropIndex("dbo.Vouchers", new[] { "Wallet_Id" });
            DropIndex("dbo.Vouchers", new[] { "Status_Id" });
            DropIndex("dbo.Vouchers", new[] { "BatchId" });
            DropIndex("dbo.Vouchers", new[] { "SchoolId" });
            DropIndex("dbo.FundAllocations", new[] { "SchoolId" });
            DropIndex("dbo.Schools", new[] { "SchoolAdmin_Id" });
            DropIndex("dbo.Schools", new[] { "CountyId" });
            DropIndex("dbo.Schools", new[] { "SchoolTypeId" });
            DropIndex("dbo.Batches", new[] { "SchoolTypeId" });
            DropIndex("dbo.Batches", new[] { "CountyId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Wallets");
            DropTable("dbo.VoucherPins");
            DropTable("dbo.Transactions");
            DropTable("dbo.Statuses");
            DropTable("dbo.Vouchers");
            DropTable("dbo.SchoolTypes");
            DropTable("dbo.ShoolAdmins");
            DropTable("dbo.FundAllocations");
            DropTable("dbo.Schools");
            DropTable("dbo.Counties");
            DropTable("dbo.Batches");
        }
    }
}
