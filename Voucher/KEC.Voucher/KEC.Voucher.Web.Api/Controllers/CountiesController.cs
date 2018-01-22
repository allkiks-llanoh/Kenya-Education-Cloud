using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
   
    public class CountiesController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            var counties = _uow.CountyRepository.GetAll().ToList();
          

            return counties.Any() ? Request.CreateResponse(HttpStatusCode.OK, value: counties.Select(c=> new County(c)).ToList()) :
                                  Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "No counties registered");
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var dbCounty = _uow.CountyRepository.Get(id);
            return dbCounty == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "County with the specified Id not found") :
                Request.CreateResponse(HttpStatusCode.OK, value: new County(dbCounty));
        }
        // GET api/<controller>?countycode=countycode
        public HttpResponseMessage Get(string countycode)
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