using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    public class SchoolTypesController : ApiController
    {
        private readonly IUnitOfWork _uow;
        public SchoolTypesController()
        {
            _uow = new EFUnitOfWork();
        }
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            var schools = _uow.SchoolTypeRepository
                          .GetAll().ToList();
           return schools.Any() ? Request.CreateResponse(HttpStatusCode.OK, schools.Select(s => new SchoolType(s))) :
                            Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var dbSchoolType = _uow.SchoolTypeRepository.Get(id);
            return dbSchoolType == null ? Request.CreateResponse(HttpStatusCode.NotFound) : 
                                          Request.CreateResponse(HttpStatusCode.OK, new SchoolType(dbSchoolType));
        }
    }
}