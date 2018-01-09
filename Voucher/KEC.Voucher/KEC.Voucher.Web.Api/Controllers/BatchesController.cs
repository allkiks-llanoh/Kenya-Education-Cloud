using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Services;
using KEC.Voucher.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    [RoutePrefix("api/batches")]
    public class BatchesController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();

        //GET api/<controller>?countrycode=value
        [HttpGet, Route("")]
        public IEnumerable<Batch> BatchByCountyCode(string countycode)
        {
            var batches = _uow.BatchRepository
                .Find(p => p.County.CountyCode.Equals(countycode))
                .Select(p => new Batch(p)).ToList();
            return batches;
        }
        //GET api/<controller>/batchcode
        [HttpGet, Route("{batchnumber}")]
        public Batch BatchByBatchNumber(string batchnumber)
        {
            var batches = _uow.BatchRepository
               .Find(p => p.BatchNumber.Equals(batchnumber))
               .Select(p => new Batch(p)).SingleOrDefault();
            return batches;
        }


        [HttpGet, Route("")]
        public ICollection<Batch> BatchesByCountyCodeAndSchoolTypeId(string countyCode, int typeId)
        {
            var batches = _uow.BatchRepository
                .Find(p => p.County.CountyCode.Equals(countyCode)
                && p.SchoolTypeId == typeId).Select(p => new Batch(p)).ToList();
            return batches;
        }
        //POST api/<controller>
        [HttpPost, Route("")]
        public HttpResponseMessage Post(BatchParam batchParam)
        {
          
            var countycode = batchParam.CountyCode;
            var SchoolTypeId = batchParam.SchoolTypeId;
            if(countycode==null|| SchoolTypeId==0)
            {
              return  Request.CreateResponse(HttpStatusCode.Forbidden, "Invalid school type or county code");
            }
            var county = _uow.CountyRepository.Find(p => p.CountyCode.Equals(countycode)).FirstOrDefault();
            var schoolType = _uow.SchoolTypeRepository.Find(p => p.Id.Equals(SchoolTypeId)).FirstOrDefault();
            var requestError = Request.CreateErrorResponse(HttpStatusCode.Forbidden, new Exception("Invalid County or School Type"));
            if (county == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid county code");
            }
            if (schoolType == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid school type");
            }

            if (county.Batches.Any((x =>  x.SchoolTypeId.Equals(SchoolTypeId) && x.Year.Equals(DateTime.Now.Year))))
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, $"Batch already exists for {county.CountyName} county for the year {DateTime.Now.Year}");
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

                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex.Message);
            }

        }
    }
}
