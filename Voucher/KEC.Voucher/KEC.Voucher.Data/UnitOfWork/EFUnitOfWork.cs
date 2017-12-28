using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.Repositories;

namespace KEC.Voucher.Data.UnitOfWork
{
   public class EFUnitOfWork : IUnitOfWork
    {
        private readonly VoucherDb _context;

        public EFUnitOfWork()
        {
            _context = new VoucherDb();
            BatchRepository = new BatchRepository(_context);
            CountyRepository = new CountyRepository(_context);
            OrderRepository = new OrderRepository(_context);
            SchoolAdminRepository = new SchoolAdminRepository(_context);
            SchoolRepository = new SchoolRepository(_context);
            SchoolTypeRepository = new SchoolTypeRepository(_context);
            StatusRepository = new StatusRepository(_context);
            TransactionRepository = new TransactionRepository(_context);
            VoucherRepository = new VoucherRepository(_context);
            WalletRepository = new WalletRepository(_context);
            FundAllocationRespository = new FundAllocationRespository(_context);
        }

        public EFUnitOfWork(DbVoucher dbVoucher)
        {
        }

        public BatchRepository BatchRepository { get; private set; }

        public CountyRepository CountyRepository { get; private set; }

        public OrderRepository OrderRepository { get; private set; }

        public SchoolAdminRepository SchoolAdminRepository { get; private set; }

        public SchoolRepository SchoolRepository { get; private set; }

        public SchoolTypeRepository SchoolTypeRepository { get; private set; }

        public StatusRepository StatusRepository { get; private set; }

        public TransactionRepository TransactionRepository { get; private set; }

        public VoucherRepository VoucherRepository { get; private set; }

        public VoucherPinRepository VoucherPinRepository { get; private set; }

        public WalletRepository WalletRepository { get; private set; }

        public FundAllocationRespository FundAllocationRespository { get; private set; }

        public int Complete()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
