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
            var schools = _uow.SchoolRepository.Find(p => p.CountyId.Equals(county.Id) && p.SchoolTypeId.Equals(batch.SchoolTypeId)).ToList();




            var vouchersList = new List<DbVoucher>();

            foreach (var school in schools)
            {
                var voucher = new DbVoucher
                {
                    VoucherSerial = Guid.NewGuid().ToString(),
                    VoucherYear = DateTime.Now.Year,
                    BatchId = batch.Id,
                    SchoolId = school.Id
                };

                voucher.VoucherCode = _uow.VoucherRepository.GetVoucherCode(batch.BatchNumber);

                voucher.Wallet = new DbWallet
                {
                    WalletAmount = school.AllocatedAmount,
                    Balance = school.AllocatedAmount,
                    UpdatedOnUtc = DateTime.UtcNow,
                    CreatedOnUtc = DateTime.UtcNow
                };

                vouchersList.Add(voucher);
            }
             

            var schoolsWithNoAllocation = vouchersList.Where(p => p.Wallet.WalletAmount == 0).Select(p=> $"{p.School.SchoolName}:{p.School.SchoolCode}");
            if (schoolsWithNoAllocation.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, $"Some of the selected schools dont have fund allocations for the year {DateTime.Now}: ${string.Join(",",schoolsWithNoAllocation)}");
            }
            _uow.VoucherRepository.AddRange(vouchersList);
            _uow.Complete();
            var vouchers = vouchersList.Select(p => new Models.Voucher(p));
            return Request.CreateResponse(HttpStatusCode.Created, vouchers);

        }
    }
}
