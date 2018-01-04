using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<SchoolType> Get()
        {
            return _uow.SchoolTypeRepository
                .GetAll().Select(p => new SchoolType(p)).ToList();
        }

        // GET api/<controller>/5
        public SchoolType Get(int id)
        {
            var dbSchoolType = _uow.SchoolTypeRepository.Get(id);
            return dbSchoolType == null ? null : new SchoolType(dbSchoolType);
        }
    }
}