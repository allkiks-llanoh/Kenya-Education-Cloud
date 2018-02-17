using System;
using System.Collections.Generic;
using System.Linq;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/SubjectTypes")]
    public class SubjectTypesController : Controller
    {
        private readonly IUnitOfWork _uow;

        public SubjectTypesController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/SubjectTypes
     
        [HttpGet]
        public IActionResult AllSubjectTypes()
        {
            var subjectTypes = _uow.SubjectTypeRepository.GetAll().ToList();
            var subjectTypesList = subjectTypes.Any() ? 
                subjectTypes.Select(p => new SubjectTypeDownloadSerializer(p)).ToList() : new List<SubjectTypeDownloadSerializer>();
            return Ok(value: subjectTypesList);
        }

        // GET: api/SubjectTypes/5
        [HttpGet("{id}", Name = "SubjectTypeById")]
        public IActionResult SubjectTypeById(int id)
        {
            var subjectType = _uow.SubjectTypeRepository.Get(id);
            if (subjectType == null)
            {
                return NotFound();
            }
            return Ok(value: new SubjectTypeDownloadSerializer(subjectType));
        }

        // POST: api/SubjectTypes
        [Authorize]
        [HttpPost]
        public IActionResult CreateSubjectType([FromBody] SubjectTypeUploadSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var exists = _uow.SubjectTypeRepository.Find(p => p.Name.Equals(model.Name)).Any();
            if (exists)
            {
                return BadRequest(error: "Subject type already exists");
            }
            var subjectType = new SubjectType
            {
                Name = model.Name,
                CreatedAtUtc = DateTime.Now.ToUniversalTime(),
                UpdatedAtUtc = DateTime.Now.ToUniversalTime()

            };
            _uow.SubjectTypeRepository.Add(subjectType);
            _uow.Complete();
            return Ok("Subject type created successfully");
        }
        
        // PUT: api/SubjectTypes/5
        [HttpPut("{id}")]
        public IActionResult EditSubjectType(int Id,[FromBody]SubjectTypeUploadSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var subjectType = _uow.SubjectTypeRepository.Get(Id);
            if (subjectType == null)
            {
               return BadRequest(error: "Subject type record could not be retrieved for updating");
            }
            subjectType.Name = model.Name;
            subjectType.UpdatedAtUtc = DateTime.Now.ToUniversalTime();
            _uow.Complete();
            return Ok("Subject type updated successfully");
        }
        
       
    }
}
