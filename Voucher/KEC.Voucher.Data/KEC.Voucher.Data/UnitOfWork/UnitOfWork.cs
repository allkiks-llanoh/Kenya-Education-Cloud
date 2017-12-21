using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEC.Voucher.Data.Repositories;

namespace KEC.Voucher.Data.UnitOfWork
{
    class UnitOfWork : IUnitOfWork
    {
        public BatchRepository BatchRepository => throw new NotImplementedException();

        public CountyRepository CountyRepository => throw new NotImplementedException();

        public OrderRepository OrderRepository => throw new NotImplementedException();

        public SchoolAdminRepository SchoolAdminRepository => throw new NotImplementedException();

        public SchoolRepository SchoolRepository => throw new NotImplementedException();

        public SchoolTypeRepository SchoolTypeRepository => throw new NotImplementedException();

        public StatusRepository StatusRepository => throw new NotImplementedException();

        public TransactionRepository TransactionRepository => throw new NotImplementedException();

        public VoucherRepository VoucherRepository => throw new NotImplementedException();

        public VoucherPinRepository VoucherPinRepository => throw new NotImplementedException();

        public WalletRepository WalletRepository => throw new NotImplementedException();

        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
