using KEC.Curation.Data.UnitOfWork;
using KEC.Publishers.Api.Cors;
using KEC.Publishers.Api.Serializers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KEC.Publishers.Api.Controllers
{
    [AllowCrossSiteJson]
    [Produces("application/json")]
    [Route("api/SubjectTypes")]
    public class SubjectTypesController : Controller
    {
        private readonly IUnitOfWork _uow;
        public SubjectTypesController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet]
        public IActionResult AllSubjectTypes()
        {
            var subjectTypes = _uow.SubjectTypeRepository.GetAll().ToList();
            var subjectTypesList = subjectTypes.Any() ?
                subjectTypes.Select(p => new SubjectTypeDownloadSerializer(p)).ToList() : new List<SubjectTypeDownloadSerializer>();
            return Ok(value: subjectTypesList);
        } 
    }
}