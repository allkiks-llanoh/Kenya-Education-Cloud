using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    [RoutePrefix("api/fundallocations")]
    public class FundAllocationsController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();
        // GET api/<controller>
        [HttpGet,Route("{year:int?}")]
        public HttpResponseMessage FundAllocations(int year,string schoolcode=null)
        {
          List<DbFundAllocation> dbFundAllocations = null;
            if (schoolcode != null)
            {
                dbFundAllocations = _uow.FundAllocationRespository.Find(p => p.Year.Equals(year) && p.School.SchoolCode.Equals(schoolcode)).ToList();
            }
            else
            {
                dbFundAllocations = _uow.FundAllocationRespository.Find(p => p.Year.Equals(year)).ToList();
            }

            if (dbFundAllocations.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, dbFundAllocations.Select(p => new FundAllocation(p)).ToList());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "No school fund allocations matches the specified criteria");
            }
        }

        

        // PUT api/<controller>/5
        [HttpPatch,Route("")]
        public HttpResponseMessage FundAllocations(FundAllcationParam fundAllcationParam)
        {
            if(fundAllcationParam==null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Invalid fund allocation parameters");
            }
            if (fundAllcationParam.Id == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Invalid fun");
            }
           
            if (fundAllcationParam.Amount == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Invalid funds amount");
            }
            var fundAllocation = _uow.FundAllocationRespository.Get(fundAllcationParam.Id);
            if (fundAllocation == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "Fund allocation not found");
            }
            var hasVoucher = _uow.VoucherRepository.Find(p => p.SchoolId.Equals(fundAllocation.SchoolId)
                                                       && p.VoucherYear.Equals(fundAllocation.Year)
                                                       && (p.Status.StatusValue == VoucherStatus.Active || p.Status.StatusValue == VoucherStatus.Expired
                                                       || p.Status.StatusValue == VoucherStatus.Suspended)).Any();
            if (hasVoucher)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, message: "Fund allocation has a voucher and cannot be edited");
            }
            fundAllocation.Amount = fundAllcationParam.Amount;
            _uow.Complete();
            return Request.CreateResponse(HttpStatusCode.OK,"FundAllocation updated sucessfully");

        }
      
    }
}