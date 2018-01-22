using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Services.Extensions;
using KEC.Voucher.Web.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    [RoutePrefix("api/vouchers")]
    public class VouchersController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();
        [HttpGet,Route("{year:int?}")]
        public HttpResponseMessage AllVouchers(int? year=null,VoucherStatus status=VoucherStatus.Active)
        {
            var queryYear = year ?? DateTime.Now.Year;
            var vouchers = _uow.VoucherRepository
                .Find(p => p.Status.StatusValue== status && p.VoucherYear.Equals(queryYear)).ToList();
            return vouchers.Any() ? Request.CreateResponse(HttpStatusCode.OK, value: vouchers.Select(v => new Models.Voucher(v)).ToList()) :
                Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "No vouchers for the specified criteria");
        }
        [HttpGet, Route("statuses")]
        public HttpResponseMessage VoucherStatuses()
        {
            var statuses = Enum.GetValues(typeof(VoucherStatus))
                               .Cast<VoucherStatus>()
                               .Select(p=>p.GetDescription()).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, statuses);
        }
        //GET api/<controller>?countrycode=value
        [HttpGet, Route("batchnumber")]
        public HttpResponseMessage VouchersBatchNumber(string batchnumber)
        {
            var vouchers = _uow.VoucherRepository
                .Find(p => p.Batch.Equals(batchnumber)).ToList();
            return vouchers.Any() ? Request.CreateResponse(HttpStatusCode.OK, value: vouchers.Select(v => new Models.Voucher(v)).ToList()) :
                Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "There are no vouchers for the specified batch number");
        }
        //GET api/<controller>/schoolcode
        [HttpGet, Route("{schoolcode}")]
        public HttpResponseMessage VoucherBySchoolCode(string schoolcode)
        {
            var vouchers = _uow.VoucherRepository
               .Find(p => p.School.SchoolCode.Equals(schoolcode)).ToList();
            return vouchers.Any() ? Request.CreateResponse(HttpStatusCode.OK, value: vouchers.Select(v => new Models.Voucher(v))) :
                                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "There are no vouchers for the specified school");
        }

        //POST api/<controller>
        [HttpPost, Route("")]
        public HttpResponseMessage Post(VoucherParam voucherParam)
        {
            if(voucherParam==null || voucherParam.BatchId == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Please specify the vouchers batch");
            }
            
            var requestError = Request.CreateErrorResponse(HttpStatusCode.Forbidden, message: "There Was an Error When Attempting To Create Vouchers");
            var batchId = voucherParam.BatchId;

            var batch = _uow.BatchRepository.Get(batchId);
            if (batch == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "Invalid batch");
            }
            var county = batch.County;
            var schoolsCodesWithVoucher = _uow.VoucherRepository.Find(p => p.School.CountyId.Equals(county.Id)
            && p.School.SchoolTypeId.Equals(batch.SchoolTypeId) &&
            ((p.Status.StatusValue == VoucherStatus.Active || p.Status.StatusValue == VoucherStatus.Suspended ||
                                                          p.Status.StatusValue == VoucherStatus.Created || p.Status.StatusValue == VoucherStatus.Expired)
                                                          && p.VoucherYear.Equals(DateTime.Now.Year))).Select(s => s.School.SchoolCode).ToList();
            var schools = _uow.SchoolRepository.Find(p => p.CountyId.Equals(county.Id)
                                                        && p.SchoolTypeId.Equals(batch.SchoolTypeId)
                                                        && !schoolsCodesWithVoucher.Contains(p.SchoolCode)).ToList();
            var vouchersList = new List<DbVoucher>();
            var padLock = new object();
            Parallel.ForEach(schools, (school, loopState) =>
            {
                var voucher = new DbVoucher
                {
                    VoucherSerial = Guid.NewGuid().ToString(),
                    VoucherYear = DateTime.Now.Year,
                    BatchId = batch.Id,
                    SchoolId = school.Id,
                    Status = new DbStatus
                    {
                        StatusValue = VoucherStatus.Created,
                        TimeStamp = DateTime.Now
                    }
                };

                lock (padLock)
                {
                    voucher.VoucherCode = _uow.VoucherRepository.GetVoucherCode(batch.BatchNumber, vouchersList);
                    voucher.Wallet = new DbWallet
                    {
                        WalletAmount = school.AllocatedAmount,
                        Balance = school.AllocatedAmount,
                        UpdatedOnUtc = DateTime.UtcNow,
                        CreatedOnUtc = DateTime.UtcNow
                    };
                }


                lock (padLock)
                {
                    vouchersList.Add(voucher);
                }

            });
           
            var schoolsWithNoAllocation = vouchersList.Any(p => p.Wallet.WalletAmount == 0);

            if (schoolsWithNoAllocation)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, message: "Some of the selected schools dont have fund allocations for the year");
            }
            _uow.VoucherRepository.AddRange(vouchersList);
            _uow.Complete();
            var vouchers = vouchersList.Select(p => new Models.Voucher(p));
            return vouchers.Any() ? Request.CreateResponse(HttpStatusCode.Created, vouchers) :
                Request.CreateErrorResponse(HttpStatusCode.Forbidden, message: $"All vouchers have been created for the batch {batch.BatchNumber}");

        }
        //GET api/<controller>/created
        [HttpGet, Route("created")]
        public HttpResponseMessage CreatedVouchers(int batchId)
        {
           
            var vouchers = _uow.VoucherRepository.Find(v => v.BatchId.Equals(batchId) && v.VoucherYear.Equals(DateTime.Now.Year));
            
            if (vouchers.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: vouchers.Select(p => new Models.Voucher(p)).ToList());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message: $"No vouchers pending approval for the batch number");
            }
        }
        [HttpPatch, Route("approve")]
        public HttpResponseMessage ApproveVoucher(VoucherApprovalParam approvalParam)
        {
            var voucher = _uow.VoucherRepository.Get(approvalParam.VoucherId);
            if (voucher == null)
            {
              return  Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "Invalid voucher or user");
            }
            if (approvalParam.Status == VoucherStatus.Rejected)
            {
                var wallet = voucher.Wallet;
                var status = voucher.Status;
                _uow.VoucherRepository.Remove(voucher);
                _uow.Complete();
                _uow.StatusRepository.Remove(status);
                _uow.Complete();
                _uow.WalletRepository.Remove(wallet);
                _uow.Complete();
            }
            else
            {
                voucher.Status.StatusValue = approvalParam.Status;
                voucher.Status.TimeStamp = DateTime.Now;
                if (approvalParam.Status == VoucherStatus.Active)
                {
                    voucher.Status.ActivatedBy = approvalParam.UserGuid;
                }
                _uow.Complete();
            }
            var vouchers = _uow.VoucherRepository.Find(p => p.BatchId.Equals(approvalParam.BatchId)
                                                          && p.VoucherYear.Equals(DateTime.Now.Year)
                                                          && p.Status.StatusValue == VoucherStatus.Created);
            if (vouchers.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: vouchers.Select(p => new Models.Voucher(p)).ToList());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: new List<Models.Voucher>());
            }
        }
        [HttpPatch, Route("selected/accept")]
        public HttpResponseMessage AcceptSelectedVouchers(VoucherApprovalParam approvalParam)
        {
            var vouchers = _uow.VoucherRepository.Find(p => approvalParam.SelectedVouchers.Contains(p.Id)
            && p.VoucherYear.Equals(DateTime.Now.Year) && p.Status.StatusValue == VoucherStatus.Created).ToList();
            var padLock = new object();
            Parallel.ForEach(vouchers, (voucher) =>
            {
                lock (padLock)
                {
                    voucher.Status.StatusValue = VoucherStatus.Active;
                    voucher.Status.TimeStamp = DateTime.Now;
                    voucher.Status.ActivatedBy = approvalParam.UserGuid;
                    _uow.Complete();
                }
            });
            var pendingvouchers = _uow.VoucherRepository.Find(p => p.BatchId.Equals(approvalParam.BatchId)
                                                        && p.VoucherYear.Equals(DateTime.Now.Year)
                                                        && p.Status.StatusValue == VoucherStatus.Created);
            if (pendingvouchers.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: pendingvouchers.Select(p => new Models.Voucher(p)).ToList());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: new List<Models.Voucher>());
            }
        }
        [HttpPatch, Route("selected/reject")]
        public HttpResponseMessage RejectSelectedVouchers(VoucherApprovalParam approvalParam)
        {
            var vouchers = _uow.VoucherRepository.Find(p => approvalParam.SelectedVouchers.Contains(p.Id)
            && p.VoucherYear.Equals(DateTime.Now.Year) && p.Status.StatusValue == VoucherStatus.Created).ToList();
            var padLock = new object();
            Parallel.ForEach(vouchers, (voucher) =>
            {
                lock (padLock)
                {
                    voucher.Status.StatusValue = VoucherStatus.Rejected;
                    voucher.Status.TimeStamp = DateTime.Now;
                    voucher.Status.ActivatedBy = approvalParam.UserGuid;
                    _uow.Complete();
                }
            });
            var pendingvouchers = _uow.VoucherRepository.Find(p => p.BatchId.Equals(approvalParam.BatchId)
                                                        && p.VoucherYear.Equals(DateTime.Now.Year)
                                                        && p.Status.StatusValue == VoucherStatus.Created);
            if (pendingvouchers.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: pendingvouchers.Select(p => new Models.Voucher(p)).ToList());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: new List<Models.Voucher>());
            }
        }

    }
}
