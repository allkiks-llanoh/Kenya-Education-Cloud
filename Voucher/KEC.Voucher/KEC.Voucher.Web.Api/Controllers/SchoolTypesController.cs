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
        [HttpGet, Route("")]
        public HttpResponseMessage GetAll()
        {
            var schools = _uow.SchoolTypeRepository
                          .GetAll().ToList();
            return schools.Any() ? Request.CreateResponse(HttpStatusCode.OK, value: schools.Select(s => new SchoolType(s))) :
                             Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "There are no school types registered");
        }

        // GET api/<controller>
        [HttpGet, Route("count")]
        public HttpResponseMessage Count()
        {
            var schoolTypesCount = _uow.SchoolTypeRepository
                          .GetAll().Count();
            return Request.CreateResponse(HttpStatusCode.OK, value: schoolTypesCount);
        }
        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var dbSchoolType = _uow.SchoolTypeRepository.Get(id);
            return dbSchoolType == null ? Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "School type not found") :
                                          Request.CreateResponse(HttpStatusCode.OK, value: new SchoolType(dbSchoolType));
        }
    }
}