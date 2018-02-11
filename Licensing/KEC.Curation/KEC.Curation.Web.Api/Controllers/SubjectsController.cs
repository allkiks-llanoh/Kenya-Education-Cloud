using System;
using System.Collections.Generic;
using System.Linq;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{
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
                subjects.Select(p => new SubjectDownloadSerializer(p,_uow)): new List<SubjectDownloadSerializer>();
            return Ok(value: subjectList.ToList());
        }

        // GET: api/Subjects/5
        [HttpGet("{id}", Name = "SubjectById")]
        public IActionResult SubjectById(int id)
        {
            var subject = _uow.SubjectRepository.Get(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(value: new SubjectDownloadSerializer(subject, _uow));
        }

        // POST: api/Subjects
        [HttpPost]
        public IActionResult CreateSubject([FromBody]SubjectUploadSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var exist = _uow.SubjectRepository.Find(p => p.Name.Equals(model.Name)
                                                   && p.SubjectTypeId.Equals(model.SubjectTypeId)).Any();
            try
            {
                if (exist)
                {
                    return BadRequest("Subject already exists");
                }
                var subject = new Subject
                {
                    Name = model.Name,
                    SubjectTypeId = model.SubjectTypeId.Value,
                    CreatedAtUtc = DateTime.Now.ToUniversalTime(),
                    UpdatedAtUtc = DateTime.Now.ToUniversalTime()
                };
                _uow.SubjectRepository.Add(subject);
                _uow.Complete();
                return Ok("Subject created successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public IActionResult EditSubject(int Id,[FromBody]SubjectUploadSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var subject = _uow.SubjectRepository.Get(Id);
                if (subject == null)
                {
                    return NotFound("Subject could not retrieved for updating");
                }
                var exist = _uow.SubjectRepository.Find(p => p.Name.Equals(model.Name)
                                                      && p.SubjectTypeId.Equals(model.SubjectTypeId.GetValueOrDefault())
                                                      && !p.Id.Equals(Id)).Any();
                if (exist)
                {
                    return BadRequest("A different subject with the same properties exists");
                }
                subject.Name = model.Name;
                subject.UpdatedAtUtc = DateTime.Now.ToUniversalTime();
                subject.SubjectTypeId = model.SubjectTypeId.Value;
                _uow.Complete();
                return Ok("Subject updated successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,"Something went wrong while processing your request");
            }

        }

    }
}
