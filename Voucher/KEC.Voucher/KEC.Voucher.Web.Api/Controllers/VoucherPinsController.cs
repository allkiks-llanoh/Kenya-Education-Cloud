using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    [RoutePrefix("api/voucherpins")]
    public class VoucherPinsController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]string userguid, [FromBody] string voucherCode)
        {
            var voucher = _uow.VoucherRepository.Find(p => p.VoucherCode.Equals(voucherCode)).FirstOrDefault();
            var requestError = Request.CreateErrorResponse(HttpStatusCode.Forbidden, new Exception("Invalid voucher code or user guid"));
            if (voucher == null)
            {
                return requestError;
            }
            //TODO: Get user phone number 
            //TODO Confirm they are the voucher authorized user
            //TODO: Create a pin for the transaction and mark as unused
            //TODO: Send pin via sms
            //TODO: Unique pin for Voucher
            var pin = new DbVoucherPin
            {
                VoucherId = voucher.Id,
                CreatedOnUtc = DateTime.UtcNow
            };
            _uow.Complete();
            return Request.CreateResponse(HttpStatusCode.OK, pin.Pin);
        }

    }
}