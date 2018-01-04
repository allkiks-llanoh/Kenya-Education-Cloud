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
    public class TransactionsController : ApiController
    {
        public readonly IUnitOfWork _uow;
        public TransactionsController()
        {
            _uow = new EFUnitOfWork();
        }
        // GET api/<controller>/year/schoolcode
        [HttpGet, Route("{year}/{schoolcode}")]
        public IEnumerable<Transaction> Get(int year, string schoolcode)
        {
          var transactions =  _uow.TransactionRepository.Find(p => p.CreatedOnUtc.Year.Equals(year)
                                         && p.Voucher.School.SchoolCode.Equals(schoolcode))
                                         .Select(p => new Transaction(p)).ToList();
            return transactions;
        }

        // GET api/<controller>/5
        public Transaction Get(int id)
        {
            var dbTransaction = _uow.TransactionRepository.Get(id);
            return dbTransaction == null ? null : new Transaction(dbTransaction);

        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

       

       
    }
}