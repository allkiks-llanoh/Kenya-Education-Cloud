using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{

    [RoutePrefix("api/transactions")]
    public class TransactionsController : ApiController
    {
        public readonly IUnitOfWork _uow = new EFUnitOfWork();

        // GET api/<controller>/year/schoolcode
        [HttpGet, Route("voucher/{Id}")]
        public HttpResponseMessage GetVoucherTransaction(int Id)
        {
            var transactions = _uow.TransactionRepository.Find(p => p.Voucher.Id.Equals(Id)).ToList();
            return transactions.Any()? Request.CreateResponse(HttpStatusCode.OK, value: transactions.Select(t=> new Transaction(t,_uow)).ToList()):
                                        Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "There are no transactions for the specified voucher");
        }

        // GET api/<controller>/5
        public HttpResponseMessage GetTransaction(int id)
        {
            var dbTransaction = _uow.TransactionRepository.Get(id);
            return dbTransaction == null ? Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "Transaction not found") :
                Request.CreateResponse(HttpStatusCode.OK, value: new Transaction(dbTransaction, _uow));

        }

        // POST api/<controller>
        public HttpResponseMessage Post(TransactionParam transactionParam)
        {
            var voucherCode = transactionParam.VoucherCode;
            var adminGuid = transactionParam.AdminGuid;
            var transactionAmount = transactionParam.Amount;
            var transactionDescription = transactionParam.Description;
            var voucher = _uow.VoucherRepository.Find(p => p.VoucherCode.Equals(voucherCode)
                                                      && p.Status.StatusValue==VoucherStatus.Active).FirstOrDefault();
            var admin = _uow.SchoolAdminRepository.Find(p => p.guid.Equals(adminGuid)).FirstOrDefault();
            var requestError = Request.CreateErrorResponse(HttpStatusCode.Forbidden, 
                message: "Invalid voucher number or pin or School admin or transction amount or description");
            if (voucherCode == null || adminGuid == null || transactionAmount == 0 || transactionDescription == null)
            {
                return requestError;
            }


            if (admin == null)
            {
                return requestError;
            }
            //TODO: Check voucher against user GUID and check for expiry
            if (voucher == null)
            {
                return requestError;
            }
            var voucherPin = _uow.VoucherPinRepository.Find(p => p.VoucherId == voucher.Id).FirstOrDefault();
            //TODO: Check pin against expiry and mark it as used
            if (voucherPin == null)
            {
                return requestError;
            }
            if (voucher.Wallet.Balance < transactionAmount)
            {
                return requestError;
            }
            var transaction = new DbTransaction
            {
                VoucherId = voucher.Id,
                TransactionDescription = transactionDescription,
                PinId = voucherPin.Id,
                Amount = transactionAmount,
                CreatedOnUtc = DateTime.UtcNow,
                SchoolAdminId = admin.Id
            };
            voucher.Wallet.Balance -= transactionAmount;
            _uow.Complete();
            return Request.CreateResponse(HttpStatusCode.OK, value: "Transaction processed successfully");
        }

    }
}