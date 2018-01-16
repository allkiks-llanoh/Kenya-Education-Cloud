using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Services.Helpers;
using KEC.Voucher.Web.Api.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    [RoutePrefix("api/batches")]
    public class BatchesController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();

        //GET api/<controller>?countrycode=value
        [HttpGet, Route("")]
        public HttpResponseMessage BatchByCountyCode(string countycode)
        {
            var batches = _uow.BatchRepository
                .Find(p => p.County.CountyCode.Equals(countycode))
                .Select(p => new Batch(p)).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, batches);
        }
        //GET api/<controller>/batchcode
        [HttpGet, Route("{batchnumber}")]
        public HttpResponseMessage BatchByBatchNumber(string batchnumber)
        {
            var dbBatch = _uow.BatchRepository
               .Find(p => p.BatchNumber.Equals(batchnumber)).FirstOrDefault();
            return dbBatch == null ? Request.CreateResponse(HttpStatusCode.NotFound) :
                                   Request.CreateResponse(HttpStatusCode.OK, new Batch(dbBatch));
        }


        [HttpGet, Route("")]
        public HttpResponseMessage BatchesByCountyCodeAndSchoolTypeId(string countyCode, int typeId)
        {
            var dbBatches = _uow.BatchRepository
                .Find(p => p.County.CountyCode.Equals(countyCode)
                && p.SchoolTypeId == typeId).ToList();
            return dbBatches.Any() ? Request.CreateResponse(HttpStatusCode.OK, dbBatches.Select(b => new Batch(b)).ToList()) :
                                     Request.CreateResponse(HttpStatusCode.NotFound);
        }
        //POST api/<controller>
        [HttpPost, Route("")]
        public HttpResponseMessage Post(BatchParam batchParam)
        {

            var countycode = batchParam.CountyCode;
            var SchoolTypeId = batchParam.SchoolTypeId;
            if (countycode == null || SchoolTypeId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid school type or county code");
            }
            var county = _uow.CountyRepository.Find(p => p.CountyCode.Equals(countycode)).FirstOrDefault();
            var schoolType = _uow.SchoolTypeRepository.Find(p => p.Id.Equals(SchoolTypeId)).FirstOrDefault();
            var requestError = Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception("Invalid County or School Type"));
            if (county == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid county code");
            }
            if (schoolType == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid school type");
            }

            if (county.Batches.Any((x => x.SchoolTypeId.Equals(SchoolTypeId) && x.Year.Equals(DateTime.Now.Year))))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, $"Batch already exists for {county.CountyName} county for the year {DateTime.Now.Year}");
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
                return Request.CreateResponse(HttpStatusCode.Created, new Batch(batch));
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
