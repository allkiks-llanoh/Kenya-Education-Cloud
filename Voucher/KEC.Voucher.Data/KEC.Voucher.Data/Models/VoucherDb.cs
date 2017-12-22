using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KEC.Voucher.Data.Mappings;
using System.Diagnostics;

namespace KEC.Voucher.Data.Models
{
    public class VoucherDb : DbContext
    {
        //static VoucherDb()
        //{
        //    //Database.SetInitializer<VoucherDb>(new MigrateDatabaseToLatestVersion<VoucherDb, Configuration>());
        //}
        //public static VoucherDb Create()
        //{
        //    return new VoucherDb();
        //}
        public VoucherDb()
            : base("name=VoucherDb")
        {
            Database.CreateIfNotExists();
            #if (DEBUG)
                        base.Database.Log = sql => Debug.WriteLine(sql);
            #endif
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DbBatchesMap());
            modelBuilder.Configurations.Add(new DbCountiesMap());
            modelBuilder.Configurations.Add(new DbOrdersMap());
            modelBuilder.Configurations.Add(new DbSchoolsMap());
            modelBuilder.Configurations.Add(new DbSchoolTypesMap());
            modelBuilder.Configurations.Add(new DbStatusesMap());
            modelBuilder.Configurations.Add(new DbTransactionsMap());
            modelBuilder.Configurations.Add(new DbVoucherPinsMap());
            modelBuilder.Configurations.Add(new DbVouchersMap());
            modelBuilder.Configurations.Add(new DbWalletsMap());
            modelBuilder.Configurations.Add(new DbSchoolAdminMap());
        }
        public DbSet<DbBatch> Batches { get; set; }
        public DbSet<DbCounty> Counties { get; set; }
        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbSchool> Schools { get; set; }
        public DbSet<DbSchoolAdmin> SchoolAdmins { get; set; }
        public DbSet<DbSchoolType> SchoolTypes { get; set; }
        public DbSet<DbStatus> Statuses { get; set; }
        public DbSet<DbTransaction> Transactions { get; set; }
        public DbSet<DbVoucherPin> VoucherPins { get; set; }
        public DbSet<DbVoucher> Vouchers { get; set; }
        public DbSet<DbWallet> Wallets { get; set; }
    }
}
