using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using System;

namespace KEC.Voucher.Web.Api.Models
{
    public class Transaction
    {
        private readonly DbTransaction _dbTransaction;
        private readonly IUnitOfWork _uow;
        public Transaction(DbTransaction dbTransaction, IUnitOfWork uow)
        {
            _dbTransaction = dbTransaction;
            _uow = uow;
        }
        public int Id
        {
            get
            {
                return _dbTransaction.Id;
            }
        }
        public string Pin
        {
            get
            {
                return _uow.VoucherPinRepository.Get(_dbTransaction.PinId).Pin;
            }
        }
        public string TransactionDescription
        {
            get
            {
                return _dbTransaction.TransactionDescription;
            }
        }
        public decimal TransactionAmount
        {
            get
            {
                return _dbTransaction.Amount;
            }
        }

        public string TransactedBy
        {
            get
            {
                return $"{_dbTransaction?.SchoolAdmin.FirstName} " +
                    $"{_dbTransaction?.SchoolAdmin.LastName}" +
                    $"({_dbTransaction ?.SchoolAdmin?.PhoneNumber})";
            }
        }
        public DateTime TransactionDate
        {
            get
            {
                return _dbTransaction.CreatedOnUtc;
            }
        }
       
    }
}