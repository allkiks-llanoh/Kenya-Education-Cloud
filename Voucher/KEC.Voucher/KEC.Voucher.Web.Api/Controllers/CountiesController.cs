using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
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
  
    [RoutePrefix("api/counties")]
    public class CountiesController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();
        // GET api/<controller>/pending/1/2018
        [HttpGet, Route("batch/pending/{year}")]
        public HttpResponseMessage CountiesWithPendingBatches(int year)
        {
            var dbCounties = _uow.CountyRepository.GetAll();
            var counties = new List<County>();
            var padLock = new object();
            Parallel.ForEach(dbCounties, (dbCounty) =>
            {
                lock (padLock)
                {
                    if (PendingSchoolTypes(dbCounty.Id, year).Any())
                    {
                        counties.Add(new County(dbCounty));
                    }
                }
            });
            return counties.Any() ? Request.CreateResponse(HttpStatusCode.OK, counties.Distinct().ToList())
                                             : Request.CreateResponse(HttpStatusCode.OK, new List<County>());
        }
        // GET api/<controller>/created/1/2018
        [HttpGet, Route("batch/created/{year}")]
        public HttpResponseMessage BatchesCreated(int year)
        {
            var dbCounties = _uow.CountyRepository.GetAll();
            var batchesCount = _uow.BatchRepository.Find(p => p.Year.Equals(year)).Distinct().Count();
            return Request.CreateResponse(HttpStatusCode.OK, batchesCount);

        }
        // GET api/<controller>
        [HttpGet, Route("")]
        public HttpResponseMessage AllCounties()
        {
            var counties = _uow.CountyRepository.GetAll().ToList();


            return counties.Any() ? Request.CreateResponse(HttpStatusCode.OK, value: counties.Select(c => new County(c)).ToList()) :
                                  Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "No counties registered");
        }
        [HttpGet, Route("count")]
        public HttpResponseMessage CountiesCount()
        {
            var countiesCount = _uow.CountyRepository.GetAll().Count();


            return Request.CreateResponse(HttpStatusCode.OK, value: countiesCount);
        }
        // GET api/<controller>/5
        [HttpGet, Route("{Id}")]
        public HttpResponseMessage CountyId(int id)
        {
            var dbCounty = _uow.CountyRepository.Get(id);
            return dbCounty == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "County with the specified Id not found") :
                Request.CreateResponse(HttpStatusCode.OK, value: new County(dbCounty));
        }
        // GET api/<controller>?countycode=countycode
        [HttpGet, Route("")]
        public HttpResponseMessage CountyByCode(string countycode)
        {
            var dbCounty = _uow.CountyRepository
                .Find(p => p.CountyCode.Equals(countycode))
                .FirstOrDefault();
            return dbCounty == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "County with the specified code not found") :
                Request.CreateResponse(HttpStatusCode.OK, value: new County(dbCounty));
        }
        private List<DbSchoolType> PendingSchoolTypes(int countyId, int year)
        {
            var schoolTypeIdsWithBatches = _uow.BatchRepository.Find(p => p.CountyId.Equals(countyId)
                                                              && p.Year.Equals(year))
                                                              .Select(p => p.SchoolTypeId).Distinct();

            var countySchoolTypeIdsWithoutBatch = _uow.SchoolRepository.Find(p => p.CountyId.Equals(countyId)
                                                           && !schoolTypeIdsWithBatches.Contains(p.SchoolTypeId))
                                                           .Select(p => p.SchoolTypeId).Distinct();
            var pendingSchoolTypes = _uow.SchoolTypeRepository.Find(p => countySchoolTypeIdsWithoutBatch.Contains(p.Id));
            return pendingSchoolTypes.ToList();
        }
    }
}