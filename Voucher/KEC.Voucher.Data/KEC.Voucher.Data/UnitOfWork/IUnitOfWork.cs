using KEC.Voucher.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        BatchRepository BatchRepository { get; }
        CountyRepository CountyRepository { get; }
        OrderRepository OrderRepository { get; }
        SchoolAdminRepository SchoolAdminRepository { get; }
        SchoolRepository SchoolRepository { get; }
        SchoolTypeRepository SchoolTypeRepository { get; }
        StatusRepository StatusRepository { get; }
        TransactionRepository TransactionRepository { get; }
        VoucherRepository VoucherRepository { get; }
        VoucherPinRepository VoucherPinRepository { get; }
        WalletRepository WalletRepository { get; }
    
        int Complete();

    }
}
