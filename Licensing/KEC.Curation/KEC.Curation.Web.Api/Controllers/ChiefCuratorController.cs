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
    [Route(template: "api/chiefcurator")]
    public class ChiefCuratorController : Controller
    {
        private IUnitOfWork _uow;

        public ChiefCuratorController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet("unassigned/subjects")]
        public IActionResult UnassignedSubjects(string chiefCuratorGuid)
        {
            var subjectIds = _uow.PublicationRepository.Find(p => !p.FullyAssigned
                                                        && p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.Curation
                                                        && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid)).Select(p=> p.SubjectId);
            var subjects = _uow.SubjectRepository.Find(p => subjectIds.Contains(p.Id));
            var subjectList = subjects.Any() ?
               subjects.Select(p => new SubjectDownloadSerializer(p, _uow)) : new List<SubjectDownloadSerializer>();
            return Ok(value: subjectList);
        }
        [HttpGet("assigned/subjects")]
        public IActionResult AssignedSubjects(string chiefCuratorGuid)
        {
            var subjectIds = _uow.PublicationRepository.Find(p => p.FullyAssigned
                                                        && p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.Curation
                                                        && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid)).Select(p => p.SubjectId);
            var subjects = _uow.SubjectRepository.Find(p => subjectIds.Contains(p.Id));
            var subjectList = subjects.Any() ?
               subjects.Select(p => new SubjectDownloadSerializer(p, _uow)) : new List<SubjectDownloadSerializer>();
            return Ok(value: subjectList);
        }
        [HttpGet("{subjectId:int}/unassigned")]
        public IActionResult UnAssigned(int subjectId,string chiefCuratorGuid)
        {
            var publications = _uow.PublicationRepository.Find(p => !p.FullyAssigned
                                                         && p.PublicationStageLogs
                                                         .Max(l => l.Stage) == PublicationStage.Curation
                                                         && p.SubjectId.Equals(subjectId)
                                                         && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid));
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        [HttpGet("{subjectId:int}/assigned")]
        public IActionResult Assigned(int subjectId, string guid,string chiefCuratorGuid)
        {
           
            var publications = _uow.PublicationRepository.Find(p => p.FullyAssigned
                                                         && p.PublicationStageLogs
                                                         .Max(l => l.Stage) == PublicationStage.Curation
                                                         &&  p.SubjectId.Equals(subjectId)
                                                          && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid));
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        [HttpPost("{publicationId:int}/assign")]
        public IActionResult Assign(int publicationId, [FromBody] CurationContentAssignmentSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository
                                  .Find(p=> p.Id.Equals(publicationId) 
                                  && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(model.AssignedBy))
                                  .FirstOrDefault();
            if (publication == null)
            {
                return NotFound("Publication could not be retrieved for assignment.");
            }
            if (publication.FullyAssigned)
            {
                return BadRequest("Publication is fully assigned");
            }
            try
            {
                
                publication.FullyAssigned = model.FullyAssigned;
                var section = new PublicationSection
                {
                    PublicationId = publication.Id,
                    Owner = model.AssignedBy,
                    SectionDescription = model.Section==null? "Whole content": model.Section,
                    CreatedAtUtc = DateTime.UtcNow
                    
                };
                _uow.PublicationSectionRepository.Add(section);
                var assignment = new CuratorAssignment
                {
                    PublicationSectionId = section.Id,
                    CreatedUtc = DateTime.UtcNow,
                    Assignee = model.Assignee,
                    AssignedBy = model.AssignedBy
                    
                };
                
                _uow.CuratorAssignmentRepository.Add(assignment);
                _uow.Complete();
                return Ok(value: "Content assigned successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{publicationId:int}/assignments")]
        public IActionResult Assignments(int publicationId, string chiefCuratorGuid)
        {
            var assignments = _uow.CuratorAssignmentRepository.Find(p => p.PublicationSection.PublicationId.Equals(publicationId)
            && p.PublicationSection.Publication.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid));
            var assignmentList = assignments.Any() ?
                assignments.Select(p => new CurationDownloadSerializer(p, _uow)).ToList() : new List<CurationDownloadSerializer>();
            return Ok(assignmentList);
        }
        [HttpGet("~api/curator/tocurate")]
        public IActionResult ToCurate(string userGuid)
        {
            var assignmentList = CurationAssignments(userGuid, _uow);
            return Ok(value: assignmentList);
        }
        [HttpPatch("~api/curator/curate/{AssignmentId:int}")]
        public IActionResult SubmitCuration(string userGuid, int AssignmentId, [FromBody]CurationUploadSerializer model)
        {
            var assigment = _uow.CuratorAssignmentRepository.Find(p => !p.Submitted
                                                                  && p.Id == AssignmentId
                                                                  && p.Assignee.Equals(userGuid))
                                                                  .FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Record could not be retrieved");
            }
            try
            {
                assigment.Notes = model.Notes;
                assigment.Submitted = model.Submitted;
                _uow.Complete();
                return Ok(value: CurationAssignments(userGuid, _uow));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("~api/curator/curate/{Id:int}")]
        public IActionResult GetCuration(string userGuid, int Id)
        {
            var assigment = _uow.CuratorAssignmentRepository.Find(p => p.Id.Equals(Id) && !p.Submitted
                                                                && p.Assignee.Equals(userGuid)).FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Curation record could not be retrieved or has been submitted");
            }
            return Ok(value: assigment);
        }
        private List<CurationDownloadSerializer> CurationAssignments(string userGuid, IUnitOfWork uow)
        {
            var assignments = _uow.CuratorAssignmentRepository.Find(p => p.Assignee.Equals(userGuid) && !p.Submitted);
            var assigmentList = assignments.Any() ?
                assignments.Select(p => new CurationDownloadSerializer(p, uow)).ToList() : new List<CurationDownloadSerializer>();
            return assigmentList;
        }

    }
}