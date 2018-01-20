using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
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

        //GET api/<controller>?countrycode=value
        [HttpGet, Route("")]
        public HttpResponseMessage VouchersBatchNumber(string batchnumber)
        {
            var vouchers = _uow.VoucherRepository
                .Find(p => p.Batch.Equals(batchnumber)).ToList();
            return vouchers.Any() ? Request.CreateResponse(HttpStatusCode.OK, vouchers.Select(v => new Models.Voucher(v)).ToList()) :
                Request.CreateResponse(HttpStatusCode.NotFound);
        }
        //GET api/<controller>/schoolcode
        [HttpGet, Route("{schoolcode}")]
        public HttpResponseMessage VoucherBySchoolCode(string schoolcode)
        {
            var vouchers = _uow.VoucherRepository
               .Find(p => p.School.SchoolCode.Equals(schoolcode)).ToList();
            return vouchers.Any() ? Request.CreateResponse(HttpStatusCode.OK, vouchers.Select(v => new Models.Voucher(v))) :
                                    Request.CreateResponse(HttpStatusCode.NotFound);
        }

        //POST api/<controller>
        [HttpPost, Route("")]
        public HttpResponseMessage Post(VoucherParam voucherParam)
        {
            if(voucherParam==null || voucherParam.BatchId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
            var requestError = Request.CreateErrorResponse(HttpStatusCode.Forbidden, new Exception("There Was an Error When Attempting To Create Vouchers"));
            var batchId = voucherParam.BatchId;

            var batch = _uow.BatchRepository.Get(batchId);
            if (batch == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Invalid batch");
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
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Some of the selected schools dont have fund allocations for the year");
            }
            _uow.VoucherRepository.AddRange(vouchersList);
            _uow.Complete();
            var vouchers = vouchersList.Select(p => new Models.Voucher(p));
            return vouchers.Any() ? Request.CreateResponse(HttpStatusCode.Created, vouchers) :
                Request.CreateResponse(HttpStatusCode.Forbidden,$"All vouchers have been created for the batch {batch.BatchNumber}");

        }
        //GET api/<controller>/created
        [HttpGet, Route("created")]
        public HttpResponseMessage CreatedVouchers(int batchId,int pageIndex,int pageSize=20)
        {
            var vouchers = _uow.VoucherRepository.GetCreatedVouchers(batchId, pageIndex, pageSize);
            var totalCount = _uow.VoucherRepository.Find(v => v.BatchId.Equals(batchId) && v.VoucherYear.Equals(DateTime.Now.Year)).Count();
            var previousPage = pageIndex > 1 ? "Yes" : "No";
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var nextPage = pageIndex < totalPages ? "Yes" : "No";
            var paginationMetadata = new
            {
                RecordCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageIndex,
                TotalPages = totalPages,
                PreviousPage= previousPage,
                NextPage= nextPage
            };

            if (vouchers.Any())
            {
                Request.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationMetadata));
                return Request.CreateResponse(HttpStatusCode.OK, vouchers.Select(p => new Models.Voucher(p)).ToList());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,$"No vouchers pending approval for the batch number");
            }
        }
        [HttpPatch, Route("approve")]
        public HttpResponseMessage ApproveVoucher(VoucherApprovalParam approvalParam)
        {
            var voucher = _uow.VoucherRepository.Get(approvalParam.VoucherId);
            if (voucher == null)
            {
              return  Request.CreateResponse(HttpStatusCode.NotFound);
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
                return Request.CreateResponse(HttpStatusCode.OK, vouchers.Select(p => new Models.Voucher(p)).ToList());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<Models.Voucher>());
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
                return Request.CreateResponse(HttpStatusCode.OK, pendingvouchers.Select(p => new Models.Voucher(p)).ToList());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<Models.Voucher>());
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
                return Request.CreateResponse(HttpStatusCode.OK, pendingvouchers.Select(p => new Models.Voucher(p)).ToList());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<Models.Voucher>());
            }
        }

    }
}
