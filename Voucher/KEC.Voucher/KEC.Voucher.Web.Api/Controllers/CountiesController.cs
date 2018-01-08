using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    public class CountiesController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();
        // GET api/<controller>
        public IEnumerable<County> Get()
        {
            return _uow.CountyRepository.GetAll().Select(p => new County(p)).ToList();
        }

        // GET api/<controller>/5
        public County Get(int id)
        {
            var dbCounty = _uow.CountyRepository.Get(id);
            return dbCounty == null ? null : new County(dbCounty);
        }
        // GET api/<controller>?countycode=countycode
        public County Get(string countycode)
        {
            var dbCounty = _uow.CountyRepository
                .Find(p => p.CountyCode.Equals(countycode))
                .FirstOrDefault();
            return dbCounty == null ? null : new County(dbCounty);
        }
    }
}