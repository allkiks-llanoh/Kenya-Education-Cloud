using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
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
        public IEnumerable<Models.Voucher> VouchersBatchNumber(string batchnumber)
        {
            var vouchers = _uow.VoucherRepository
                .Find(p => p.Batch.Equals(batchnumber))
                .Select(p => new Models.Voucher(p)).ToList();
            return vouchers;
        }
        //GET api/<controller>/schoolcode
        [HttpGet, Route("{schoolcode}")]
        public IList<Models.Voucher> VoucherBySchoolCode(string schoolcode)
        {
            var vouchers = _uow.VoucherRepository
               .Find(p => p.School.SchoolCode.Equals(schoolcode))
               .Select(p => new Models.Voucher(p)).ToList();
            return vouchers;
        }

        //POST api/<controller>
        [HttpPost, Route("")]
        public HttpResponseMessage Post(VoucherParam voucherParam)
        {


            var requestError = Request.CreateErrorResponse(HttpStatusCode.Forbidden, new Exception("There Was an Error When Attempting To Create Vouchers"));
            var batchId = voucherParam.BatchId;

            var batch = _uow.BatchRepository.Get(batchId);
            var county = batch.County;
            var schoolsCodesWithVoucher = _uow.VoucherRepository.Find(p => p.School.CountyId.Equals(county.Id) 
            && p.School.SchoolTypeId.Equals(batch.SchoolTypeId) && 
            ((p.Status.StatusValue == VoucherStatus.Active || p.Status.StatusValue == VoucherStatus.Suspended ||
                                                          p.Status.StatusValue == VoucherStatus.Created || p.Status.StatusValue == VoucherStatus.Expired)
                                                          && p.VoucherYear.Equals(DateTime.Now.Year))).Select(s=> s.School.SchoolCode).ToList();
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


            var schoolsWithNoAllocation = vouchersList.Where(p => p.Wallet.WalletAmount == 0).Select(p => $"{p.School.SchoolName}:{p.School.SchoolCode}");
            if (schoolsWithNoAllocation.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, $"Some of the selected schools dont have fund allocations for the year {DateTime.Now}: ${string.Join(",", schoolsWithNoAllocation)}");
            }
            _uow.VoucherRepository.AddRange(vouchersList);
            _uow.Complete();
            var vouchers = vouchersList.Select(p => new Models.Voucher(p));
            return Request.CreateResponse(HttpStatusCode.Created, vouchers);

        }
        //GET api/<controller>/approval
        [HttpGet, Route("created")]
        public IEnumerable<Models.Voucher> CreatedVouchers(int batchId)
        {
            var vouchers = _uow.VoucherRepository.Find(p => p.BatchId.Equals(batchId)
                                                            && p.VoucherYear.Equals(DateTime.Now.Year)
                                                            && p.Status.StatusValue==VoucherStatus.Created);
            return vouchers.Any() ? vouchers.Select(p => new Models.Voucher(p)).ToList() : null;
        }
        [HttpPatch, Route("approve")]
        public IEnumerable<Models.Voucher> ApproveVoucher(VoucherApprovalParam approvalParam)
        {
            var voucher = _uow.VoucherRepository.Get(approvalParam.VoucherId);
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
                                                          && p.Status.StatusValue==VoucherStatus.Created);
            return vouchers.Any() ? vouchers.Select(p => new Models.Voucher(p)).ToList() : null;
        }
        [HttpPatch, Route("selected/activate")]
        public IEnumerable<Models.Voucher> ActivateSelectedVoucher(VoucherApprovalParam approvalParam)
        {
            var vouchers = _uow.VoucherRepository.Find(p => approvalParam.SelectedVouchers.Contains(p.Id)
            && p.VoucherYear.Equals(DateTime.Now.Year) && p.Status.StatusValue==VoucherStatus.Created).ToList();
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
                                                        && p.Status.StatusValue==VoucherStatus.Created);
            return pendingvouchers.Any() ? pendingvouchers.Select(p => new Models.Voucher(p)).ToList() : null;
        }


    }
}
