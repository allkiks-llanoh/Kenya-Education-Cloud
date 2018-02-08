using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Services.Helpers;
using KEC.Voucher.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{ 
    [Authorize]
    [RoutePrefix("api/batches")]
    public class BatchesController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();

        //GET api/<controller>?countrycode=value
        [HttpGet, Route("{year:int?}")]
        public HttpResponseMessage AllBatches(int? year = null)
        {
            var queryYear = year ?? DateTime.Now.Year;
            var DbBatches = _uow.BatchRepository
                .Find(p => p.Year.Equals(queryYear));
            var batches = DbBatches.Any() ? DbBatches.Select(p => new Batch(p)) : new List<Batch>();
            return Request.CreateResponse(HttpStatusCode.OK, value: batches);
        }
        [HttpGet, Route("{year:int?}/withpendingvouchers")]
        public HttpResponseMessage WithPendingVouchers(int? year = null)
        {
            var queryYear = year ?? DateTime.Now.Year;
            var batches = BatchesWithPendingVouchers(queryYear);
            return Request.CreateResponse(HttpStatusCode.OK, value: batches);
        }
        [HttpGet, Route("{year:int?}/withpendingvouchers/count")]
        public HttpResponseMessage WithPendingVouchersCount(int? year = null)
        {
            var queryYear = year ?? DateTime.Now.Year;
            var batches = BatchesWithPendingVouchers(queryYear);
            return Request.CreateResponse(HttpStatusCode.OK, value: batches.Count());
        }
        [HttpGet, Route("count")]
        public HttpResponseMessage BatchesCount()
        {

            var DbBatchesCount = _uow.BatchRepository
                .Find(p => p.Year.Equals(DateTime.Now.Year)).Count();
            return Request.CreateResponse(HttpStatusCode.OK, value: DbBatchesCount);
        }
        [HttpGet, Route("{countyId}/pendingschooltypes")]
        public HttpResponseMessage PendingCountySchoolTypeBatches(int countyId)
        {
            var schoolTypeIdsWithBatches = _uow.BatchRepository.Find(p => p.CountyId.Equals(countyId)
                                                              && p.Year.Equals(DateTime.Now.Year))
                                                              .Select(p => p.SchoolTypeId).Distinct();

            var countySchoolTypeIdsWithoutBatch = _uow.SchoolRepository.Find(p => p.CountyId.Equals(countyId)
                                                           && !schoolTypeIdsWithBatches.Contains(p.SchoolTypeId))
                                                           .Select(p => p.SchoolTypeId).Distinct();
            var pendingSchoolTypes = _uow.SchoolTypeRepository.Find(p => countySchoolTypeIdsWithoutBatch.Contains(p.Id));
            return Request.CreateResponse(HttpStatusCode.OK, value: pendingSchoolTypes.Any() ?
                                                             pendingSchoolTypes.Select(p => new SchoolType(p)).ToList() : new List<SchoolType>());
        }
        //GET api/<controller>/batchcode
        [HttpGet, Route("{batchnumber}")]
        public HttpResponseMessage BatchByBatchNumber(string batchnumber)
        {
            var dbBatch = _uow.BatchRepository
               .Find(p => p.BatchNumber.Equals(batchnumber)).FirstOrDefault();
            return dbBatch == null ? Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "Batch not found") :
                                   Request.CreateResponse(HttpStatusCode.OK, new Batch(dbBatch));
        }


        [HttpGet, Route("")]
        public HttpResponseMessage BatchesByCountyCodeAndSchoolTypeId(string countyCode, int typeId)
        {
            var dbBatches = _uow.BatchRepository
                .Find(p => p.County.CountyCode.Equals(countyCode)
                && p.SchoolTypeId == typeId).ToList();
            return dbBatches.Any() ? Request.CreateResponse(HttpStatusCode.OK, dbBatches.Select(b => new Batch(b)).ToList()) :
                                     Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "Batches for the selected county and school type not found");
        }
        //POST api/<controller>
        [HttpPost, Route("")]
        public HttpResponseMessage Post(BatchParam batchParam)
        {

            var countycode = batchParam.CountyCode;
            var SchoolTypeId = batchParam.SchoolTypeId;
            if (countycode == null || SchoolTypeId == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Invalid school type or county code");
            }
            var county = _uow.CountyRepository.Find(p => p.CountyCode.Equals(countycode)).FirstOrDefault();
            var schoolType = _uow.SchoolTypeRepository.Find(p => p.Id.Equals(SchoolTypeId)).FirstOrDefault();
            var requestError = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Invalid County or School Type");
            if (county == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Invalid county code");
            }
            if (schoolType == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Invalid school type");
            }

            if (county.Batches.Any((x => x.SchoolTypeId.Equals(SchoolTypeId) && x.Year.Equals(DateTime.Now.Year))))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    message: $"Batch already exists for {schoolType.SchoolType} " +
                    $"schools in {county.CountyName} county for the year {DateTime.Now.Year}");
            }
            try
            {
                var batch = new DbBatch
                {
                    CountyId = county.Id,
                    SchoolTypeId = SchoolTypeId,

                    BatchNumber = RandomCodeGenerator.GetBatchNumber(county.CountyCode),
                    SerialNumber = Guid.NewGuid().ToString(),
                    Year = DateTime.Now.Year,
                };
                _uow.BatchRepository.Add(batch);
                _uow.Complete();
                return Request.CreateResponse(HttpStatusCode.Created, value: batch.BatchNumber);
            }
            catch (Exception)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message: "Internal server error");
            }

        }
        private bool HasPendingVouchers(DbBatch dbBatch)
        {
            var schoolIdsWithVoucher = _uow.VoucherRepository.Find(p => p.BatchId.Equals(dbBatch.Id) 
                                                              && p.Status.StatusValue!=VoucherStatus.Rejected)
                                                                 .ToList().Select(p=> p.SchoolId);
            var schoolIdsWithoutVocher = _uow.FundAllocationRespository.Find(p => p.School.SchoolTypeId.Equals(dbBatch.SchoolTypeId)
                                                                             && p.Year.Equals(dbBatch.Year) && p.School.CountyId.Equals(dbBatch.CountyId)
                                                                             && !schoolIdsWithVoucher.Contains(p.SchoolId));
            return schoolIdsWithoutVocher.Any();
        }
        private List<Batch> BatchesWithPendingVouchers(int queryYear)
        {
            var batches = new List<Batch>();
            var padLock = new object();
            var DbBatches = _uow.BatchRepository
                .Find(p => p.Year.Equals(queryYear)).Distinct();
            Parallel.ForEach(DbBatches, (dbBatch) =>
            {
                lock (padLock)
                {
                    if (HasPendingVouchers(dbBatch))
                    {
                        batches.Add(new Batch(dbBatch));
                    }
                }
            });
            return batches;
        }
    }
}
