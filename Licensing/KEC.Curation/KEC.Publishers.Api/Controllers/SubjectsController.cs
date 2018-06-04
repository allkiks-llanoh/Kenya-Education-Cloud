using System.Collections.Generic;
using System.Linq;
using KEC.Publishers.Api.Cors;
using KEC.Publishers.Api.Serializers;
using KEC.Publishers.Data.UnitOfWork.Core;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Publishers.Api.Controllers
{
    [AllowCrossSiteJson]
    [Produces("application/json")]
    [Route("api/Subjects")]
    public class SubjectsController : Controller
    {
        private readonly IUnitOfWork _uow;
        public SubjectsController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/Subjects
        [HttpGet]
        public IActionResult AllSubjects()
        {
            var subjects = _uow.SubjectRepository.GetAll().ToList();
            var subjectList = subjects.Any() ?
                subjects.Select(p => new SubjectDownloadSerializer(p, _uow)) : new List<SubjectDownloadSerializer>();
            return Ok(value: subjectList.ToList());
        }
    }
}