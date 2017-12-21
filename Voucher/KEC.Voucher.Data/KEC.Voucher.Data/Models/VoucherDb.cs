using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KEC.Voucher.Data.Models
{
    public class VoucherDb
    {
        static VoucherDb()
        {
            //Database.SetInitializer<VoucherDb>(new MigrateDatabaseToLatestVersion<VoucherDb, Configuration>());
        }

        //        public VoucherDb()
        //            : base("VoucherDb")
        //        {
        //#if (DEBUG)
        //            base.Database.Log = sql => Debug.WriteLine(sql);
        //#endif
        //        }

        public static VoucherDb Create()
        {
            return new VoucherDb();
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
