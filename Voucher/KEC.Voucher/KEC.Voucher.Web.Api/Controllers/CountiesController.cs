using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    [RoutePrefix("api/counties")]
    public class CountiesController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();
        // GET api/<controller>/pending/1/2018
        [HttpGet,Route("pending/schoolTypeId/{year}")]
        public HttpResponseMessage CountiesWithoutVouchers(int year,int schoolTypeId)
        {
            var countiesWithBatch = _uow.BatchRepository.Find(p => p.Year.Equals(year) && p.SchoolTypeId.Equals(schoolTypeId)).ToList().Select(p => p.CountyId);
            var countiesWithouthBatch = _uow.CountyRepository.Find(p => !countiesWithBatch.Contains(p.Id));
            return countiesWithBatch.Any() ? Request.CreateResponse(HttpStatusCode.OK, countiesWithouthBatch.Select(p => new County(p)).ToList())
                                             : Request.CreateResponse(HttpStatusCode.OK, new List<County>());
        }
        // GET api/<controller>/created/1/2018
        [HttpGet, Route("created/{schooltypeId}/{year}")]
        public HttpResponseMessage CountiesWithVouchers(int year, int schoolTypeId)
        {
            var countiesWithBatch = _uow.BatchRepository.Find(p => p.Year.Equals(year) && p.SchoolTypeId.Equals(schoolTypeId)).Count();
            return Request.CreateResponse(HttpStatusCode.OK, countiesWithBatch);
                                           
        }
        // GET api/<controller>
        public HttpResponseMessage AllCounties()
        {
            var counties = _uow.CountyRepository.GetAll().ToList();


            return counties.Any() ? Request.CreateResponse(HttpStatusCode.OK, value: counties.Select(c => new County(c)).ToList()) :
                                  Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "No counties registered");
        }

        // GET api/<controller>/5
        public HttpResponseMessage CountyId(int id)
        {
            var dbCounty = _uow.CountyRepository.Get(id);
            return dbCounty == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "County with the specified Id not found") :
                Request.CreateResponse(HttpStatusCode.OK, value: new County(dbCounty));
        }
        // GET api/<controller>?countycode=countycode
        public HttpResponseMessage CountyByCode(string countycode)
        {
            var dbCounty = _uow.CountyRepository
                .Find(p => p.CountyCode.Equals(countycode))
                .FirstOrDefault();
            return dbCounty == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "County with the specified code not found") :
                Request.CreateResponse(HttpStatusCode.OK, value: new County(dbCounty));
        }

    }
}