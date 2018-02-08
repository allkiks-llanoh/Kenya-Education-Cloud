﻿using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Services.AfricasTalking;
using KEC.Voucher.Services.Extensions;
using KEC.Voucher.Web.Api.Models;
using System;
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
        public HttpResponseMessage Post(PinParam pinParam)
        {
            var voucherCode = pinParam.VoucherCode;
            var userguid = pinParam.UserGuid;
            var requestError = Request.CreateErrorResponse(HttpStatusCode.Forbidden, message: "Invalid voucher code or user guid");
            if (voucherCode == null || userguid == null)
            {
                return requestError;
            }
            var voucher = _uow.VoucherRepository.Find(p => p.VoucherCode.Equals(voucherCode)
                            && p.Status.StatusValue==VoucherStatus.Active).FirstOrDefault();
            if (voucher == null)
            {
                return requestError;
            }
            var schoolAdmin = _uow.SchoolAdminRepository.Find(p => p.guid.Equals(userguid)).FirstOrDefault();
            if (voucher.School.SchoolAdmin.Id != schoolAdmin.Id)
            {
                return requestError;
            }
            if (schoolAdmin == null)
            {
                return requestError;
            }
            try
            {
                var pin = new DbVoucherPin
                {
                    VoucherId = voucher.Id,
                    CreatedOnUtc = DateTime.UtcNow,
                    Status = PinStatus.Pending,
                    Pin = _uow.VoucherPinRepository.GetVoucherPin()
                };
                _uow.VoucherPinRepository.Add(pin);
                _uow.Complete();
                var smsService = new AfricasTalkingSmsService();
                if (!schoolAdmin.PhoneNumber.PhoneValid())
                {
                   return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Invalid School admin phone number");
                }
                smsService.SendSms(schoolAdmin.PhoneNumber, pin.Pin);
                return Request.CreateResponse(HttpStatusCode.OK, value: "Sending voucher pin sms");
            }
            catch (Exception)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message: "Internal server error");
            }
        }

    }
}