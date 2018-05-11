using System;
using System.Collections.Generic;
using System.Linq;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Cors;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
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
                subjects.Select(p => new SubjectDownloadSerializer(p,_uow)): new List<SubjectDownloadSerializer>();
            return Ok(value: subjectList.ToList());
        }
        // GET: api/Subjects
        [HttpGet("ForCurators")]
        public IActionResult CuratorSubjects()
        {
            var subjects = _uow.SubjectRepository.GetAll().ToList();
            var subjectList = subjects.Any() ?
                subjects.Select(p => new SubjectDownloadSerializerForCurators(p, _uow)) : new List<SubjectDownloadSerializerForCurators>();
            return Ok(value: subjectList.ToList());
        }
        // GET: api/Listing/Levels
        [HttpGet("Listing/Levels")]
        public IActionResult ListLevels()
        {
            var subjects = _uow.LevelRepository.GetAll().ToList();
            var subjectList = subjects.Any() ?
                subjects.Select(p => new LevelsDownloadSerilizer(p, _uow)) : new List<LevelsDownloadSerilizer>();
            return Ok(value: subjectList.ToList());
        }
        // GET: api/Listing/Levels
        [HttpGet("Listing/Category")]
        public IActionResult ListCategory()
        {
            var subjects = _uow.SubjectTypeRepository.GetAll().ToList();
            return Ok(subjects);
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
        [HttpPatch("subject/{id}")]
        public IActionResult EditSubject(int Id,[FromBody]SubjectUploadSerializerEdit model)
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
                
              
                subject.Name = model.Name;
                subject.UpdatedAtUtc = DateTime.Now.ToUniversalTime();
                _uow.Complete();
                return Ok("Subject updated successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,"Something went wrong while processing your request");
            }

        }
        [HttpDelete("{id}")]

        public IActionResult DeleteSubject(DeleteSerilizer model)
        {

            var level = _uow.SubjectRepository.Get(model.Id.GetValueOrDefault());
            if (level == null)
            {
                return NotFound("Level could not be retrieved for deleting or is missing ");
            }
            _uow.SubjectRepository.Remove(level);

            _uow.Complete();
            return Ok("Subject Deleted From Repository");
        }

    }
}
