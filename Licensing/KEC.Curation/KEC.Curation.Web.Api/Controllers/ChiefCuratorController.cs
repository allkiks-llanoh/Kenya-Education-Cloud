using System;
using System.Collections.Generic;
using System.Linq;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Cors;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KEC.Curation.Services.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KEC.Curation.Web.Api.Controllers
{
    [AllowCrossSiteJson]
    [Produces("application/json")]
    [Route(template: "api/chiefcurator")]
    public class ChiefCuratorController : Controller
    {
        private IUnitOfWork _uow;
        public ChiefCuratorController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        #region Chief curator

        #region Subjects
        [HttpGet("unassigned/subjects")]
        public IActionResult UnassignedSubjects(string chiefCuratorGuid)
        {
            var subjectIds = _uow.PublicationRepository.Find(p => !p.FullyAssigned
                                                        && p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.Curation
                                                        && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid)).Select(p => p.SubjectId);
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
        [HttpGet("curationhistory/subjects")]
        public IActionResult CurationHistorySubjects(string chiefCuratorGuid)
        {
            var subjectIds = _uow.PublicationRepository.Find(p => p.FullyAssigned
                                                        && p.PublicationStageLogs.Any(l => l.Stage == PublicationStage.Curation && l.Notes != null)
                                                        && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid)).Select(p => p.SubjectId);
            var subjects = _uow.SubjectRepository.Find(p => subjectIds.Contains(p.Id));
            var subjectList = subjects.Any() ?
               subjects.Select(p => new SubjectDownloadSerializer(p, _uow)) : new List<SubjectDownloadSerializer>();
            return Ok(value: subjectList);
        }
        #endregion
        /// <summary>
        /// Get unassigned publications
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="chiefCuratorGuid"></param>
        /// <returns></returns>
        #region Assignments
        [HttpGet("publications/{subjectId:int}/unassigned")]
        public IActionResult UnAssigned(int subjectId, string chiefCuratorGuid)
        {
            var publications = _uow.PublicationRepository.Find(p => !p.FullyAssigned
                                                         && p.PublicationStageLogs
                                                         .Max(l => l.Stage) == PublicationStage.Curation
                                                         && p.SubjectId.Equals(subjectId)
                                                         && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid));
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizerToCurators(p, _uow)).ToList() : new List<PublicationDownloadSerilizerToCurators>();
            return Ok(value: publicationList);
        }
        [HttpGet("publications/{subjectId:int}/unassignedNew")]
        public IActionResult UnAssignedNew(int subjectId, string chiefCuratorGuid)
        {
            var publications = _uow.ChiefCuratorAssignmentRepository.Find(p => !p.Submitted && p.ChiefCuratorGuid.Equals(chiefCuratorGuid));
            var publicationList = publications.Any() ?
                publications.Select(p => new ChiefCutatorDownloadSerilizer(p, _uow)).ToList() : new List<ChiefCutatorDownloadSerilizer>();
            return Ok(value: publicationList);
        }

        /// <summary>
        /// Get assigned publications
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="chiefCuratorGuid"></param>
        /// <returns></returns>
        [HttpGet("publications/{subjectId:int}/assigned")]
        public IActionResult Assigned(int subjectId, string chiefCuratorGuid)
        {

            var publications = _uow.ChiefCuratorAssignmentRepository.Find(p => p.ChiefCuratorGuid.Equals(chiefCuratorGuid)).ToList();
            var assignmentList = publications.Any() ?
            publications.Select(p => new ChiefCutatorDownloadSerilizer(p, _uow)).ToList() : new List<ChiefCutatorDownloadSerilizer>();
            return Ok(assignmentList);

        }
        [HttpGet("publications/{subjectId:int}/comments")]
        public IActionResult AssignedWithComments(int subjectid, string chiefcuratorguid)
        {

            var publications = _uow.ChiefCuratorAssignmentRepository.Find(p => p.ChiefCuratorGuid.Equals(chiefcuratorguid)
                              && p.Publication.PublicationStageLogs.Max(l => l.Stage) == PublicationStage.Curation
                              && !p.Submitted).ToList();
            var assignmentList = publications.Any() ?
            publications.Select(p => new ChiefCutatorDownloadSerilizer(p, _uow)).ToList() : new List<ChiefCutatorDownloadSerilizer>();
            return Ok(assignmentList);

        }
        [HttpPost("publication/{publicationId:int}/assign")]
        public IActionResult Assign(int publicationId, [FromBody] CurationContentAssignmentSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository
                                  .Find(p => p.Id.Equals(publicationId)
                                  && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(model.AssignedBy))
                                  .FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication could not be retrieved for assignment." });
            }
            if (publication.FullyAssigned)
            {
                return BadRequest(error: new { message = "Publication is fully assigned" });
            }
            var exists = _uow.CuratorAssignmentRepository.Find(p => p.PublicationId.Equals(publication.Id)
                         && p.Assignee.Equals(model.Assignee)).Any();

            if (exists)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            try
            {
                var section = new PublicationSection
                {
                    PublicationId = publication.Id,
                    Owner = model.AssignedBy,
                    SectionDescription = model.Section == null ? "Whole content" : model.Section,
                    CreatedAtUtc = DateTime.UtcNow
                };
                _uow.PublicationSectionRepository.Add(section);
                var assignment = new CuratorAssignment
                {
                    PublicationSectionId = section.Id,
                    CreatedUtc = DateTime.UtcNow,
                    Assignee = model.Assignee,
                    AssignedBy = model.AssignedBy,
                    PublicationId = publication.Id,
                };
                _uow.CuratorAssignmentRepository.Add(assignment);
                var _new = _uow.ChiefCuratorAssignmentRepository.Find(p => p.PublicationId.Equals(publication.Id)).FirstOrDefault();
                _new.Assigned = bool.Parse("true");
                _uow.Complete();
                return Ok(value: new { message = "Content assigned successfully" });
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("publication/{publicationId:int}/assignments")]
        public IActionResult Assignments(int publicationId, string chiefCuratorGuid)
        {
            var assignments = _uow.CuratorAssignmentRepository.Find(p => p.PublicationSection.PublicationId.Equals(publicationId)
            && p.PublicationSection.Publication.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid));
            var assignmentList = assignments.Any() ?
                assignments.Select(p => new CurationDownloadSerializer(p, _uow)).ToList() : new List<CurationDownloadSerializer>();
            return Ok(assignmentList);
        }
        [HttpGet("UnAssignedPublication/{id}")]
        public IActionResult UnAssignedPublication(int Id, string chiefCuratorGuid)
        {
            var publication = _uow.PublicationRepository.Find(p => !p.FullyAssigned
                                                        && p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.Curation
                                                        && p.Id.Equals(Id)
                                                         && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid)).FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication record could not be retrieved" });
            }
            return Ok(value: new PublicationDownloadSerilizerToCurators(publication, _uow));
        }
        [HttpGet("UnAssignedPublications/{id}")]
        public IActionResult UnAssignedPublications(int Id, string chiefCuratorGuid)
        {
            var publication = _uow.PublicationRepository.Find(p => !p.FullyAssigned
                                                        && p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.Curation
                                                        && p.Id.Equals(Id)
                                                         && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid)).FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication record could not be retrieved" });
            }
            return Ok(value: new PublicationDownloadSerilizerGetUrl(publication, _uow));
        }
        [HttpDelete("publication/assignment/{id}")]
        public IActionResult DeleteUnAssignedPublication(int Id, [FromBody]string chiefCuratorGuid)
        {
            try
            {
                var assignment = _uow.CuratorAssignmentRepository.Find(p => !p.Submitted && p.AssignedBy.Equals(chiefCuratorGuid) && p.Id.Equals(Id)).FirstOrDefault();
                if (assignment == null)
                {
                    return BadRequest(error: new { message = "Curator assigment cannot be deleted" });
                }
                _uow.CuratorAssignmentRepository.Remove(assignment);
                _uow.Complete();
                return Ok(value: new { message = "Curator assignment deleted successfully" });
            }
            catch (Exception)
            {
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("AssignedPublication/{id}")]
        public IActionResult AssignedPublication(int Id, string chiefCuratorGuid)
        {
            var publication = _uow.PublicationRepository.Find(p => p.FullyAssigned
                                                        && p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.Curation
                                                        && p.Id.Equals(Id)
                                                         && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid)).FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication record could not be retrieved" });
            }
            return Ok(value: new PublicationDownloadSerilizerToCurators(publication, _uow));
        }
        [HttpGet("AssignedPublicationWhenGettingCuration/{id}")]
        public IActionResult AssignedPublicationWhenGettingCuration(int Id, string chiefCuratorGuid)
        {

            var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(Id)).FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication record could not be retrieved" });
            }
            return Ok(value: new PublicationDownloadSerilizerToCurators(publication, _uow));
        }
        #endregion

        #region Curator Submissions
        [HttpGet("publication/{publicationId:int}/curatorsubmissions")]
        public IActionResult CuratorSubmissions([FromQuery]int publicationId, string chiefCuratorGuid)
        {
            var assignmentSubmissions = _uow.CuratorAssignmentRepository.Find(p => p.PublicationId.Equals(publicationId)
            && p.AssignedBy.Equals(chiefCuratorGuid) && p.Submitted);
            var assignmentList = assignmentSubmissions.Any() ?
                assignmentSubmissions.Select(p => new CurationDownloadSerializer(p, _uow)).ToList() : new List<CurationDownloadSerializer>();
            return Ok(assignmentList);
        }
        [HttpGet("publication/{publicationId:int}/curatorsubmissionsToGetAssignee")]
        public IActionResult curatorsubmissionsToGetAssignee([FromQuery]int publicationId)
        {
            var assignmentSubmissions = _uow.CuratorAssignmentRepository.Find(p => p.PublicationId.Equals(publicationId));
            var assignmentList = assignmentSubmissions.Any() ?
                assignmentSubmissions.Select(p => new CurationRepoDownloadSerilizer(p, _uow)).ToList() : new List<CurationRepoDownloadSerilizer>();
            return Ok(assignmentList);
        }
        #endregion
        #region Curator Comments For Principal Curators get all comments with full names 
        [HttpGet("publication/{publicationId:int}/comments")]
        public IActionResult CuratorSubmissionsPrincipal([FromQuery]int publicationId)
        {
            var curationComments = _uow.ChiefCuratorCommentRepository.Find(p => p.PublicationId.Equals(publicationId)
                                   && p.Submitted == false);
            var curationCommentList = curationComments.Any() ?
                curationComments.Select(p => new CurationCommentSerializer(p, _uow)).ToList() : new List<CurationCommentSerializer>();
            return Ok(curationCommentList);

        }
        [HttpGet("publication/{publicationId:int}/curatorcomments")]
        public IActionResult CuratorSubmissionsAndComments([FromQuery]int publicationId)
        {

            var curationComments = _uow.CuratorAssignmentRepository.Find(p => p.PublicationId.Equals(publicationId)
                                   && p.Submitted);
            var curationCommentList = curationComments.Any() ?
                curationComments.Select(p => new CurationRepoDownloadSerilizer(p, _uow)).ToList() : new List<CurationRepoDownloadSerilizer>();
            return Ok(curationCommentList);

        }
        [HttpGet("publication/withcomments")]
        public IActionResult WithCommentsAtChiefCuratorLevel([FromQuery]string userId)
        {

            var assignment = _uow.ChiefCuratorAssignmentRepository.Find(p => p.ChiefCuratorGuid.Equals(userId) && !p.Submitted && p.Assigned);
            var assignmentList = assignment.Any() ?
             assignment.Select(p => new ChiefCutatorDownloadSerilizer(p, _uow)).ToList() : new List<ChiefCutatorDownloadSerilizer>();
            return Ok(assignmentList);


        }
        [HttpPatch("update/curatorcomments/{id}")]
        public IActionResult UpdateCurationComments(int publicationId, [FromBody]ChiefFlagSubmittedSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var assigment = _uow.PublicationRepository.Find(p => !p.FullyAssigned
                                                                  && p.Id.Equals(model.publicationId)
                                                                  && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(model.UserGuid))
                                                                  .FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Record could not be retrieved");
            }
            try
            {
                assigment.FullyAssigned = true;
                _uow.Complete();
                return Ok(value: new { message = "Curation Fully Assigned" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Curation History
        [HttpGet("publications/{subjectId:int}/history")]
        public IActionResult PublicationsCurationHistory(string chiefCuratorGuid, int subjectId)
        {
            var publications = _uow.PublicationRepository.Find(p => p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid)
                                && p.FullyAssigned && p.PublicationStageLogs.Any(l => l.Stage == PublicationStage.Curation && l.Notes != null)
                                && p.SubjectId.Equals(subjectId));
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizerToCurators(p, _uow)).ToList() : new List<PublicationDownloadSerilizerToCurators>();

            return Ok(value: publicationList);
        }
        [HttpGet("publication/{publicationId:int}/history")]
        public IActionResult PublicationCurationHistory(string chiefCuratorGuid, int publicationId)
        {
            var publication = _uow.PublicationRepository.Find(p => p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(chiefCuratorGuid)
                             && p.FullyAssigned && p.PublicationStageLogs.Any(l => l.Stage == PublicationStage.Curation && l.Notes != null))
                             .FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication could not be retrieved" });
            }
            return Ok(new PublicationDownloadSerilizer(publication, _uow));
        }
        #endregion
        #endregion

        #region Curator
        [HttpGet("curator/tocurate")]
        public IActionResult ToCurate(string userGuid)
        {
            var assigment = _uow.CuratorAssignmentRepository.Find(p => !p.Submitted && p.Assignee.Equals(userGuid));
            if (assigment == null)
            {
                return NotFound("Curation record could not be retrieved or has been submitted");
            }
            var assigmentList = assigment.Any() ?
               assigment.Select(p => new CurationRepoDownloadSerilizer(p, _uow)).ToList() : new List<CurationRepoDownloadSerilizer>();
            return Ok(value: assigmentList);
        }

        [HttpGet("curator/listings/{id}")]
        public IActionResult GetAllCuratorAssignments([FromQuery]int id)
        { 
            var assigment = _uow.CuratorAssignmentRepository.Find(p => p.Submitted && p.PublicationId.Equals(id)).FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Curation record could not be retrieved or has been submitted");
            }
            return Ok(value: new CurationRepoDownloadSerilizer(assigment, _uow));
        }
        [HttpGet("curator/getlistings/{id}")]
        public IActionResult ListAllCuratorAssignments([FromQuery]int id)
        {
            var assigment = _uow.CuratorAssignmentRepository.Find(p => p.Id.Equals(id)).FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Curation record could not be retrieved or has been submitted");
            }
            return Ok(value: new CurationRepoDownloadSerilizer(assigment, _uow));
        }
        [HttpGet("getlistingsChifCurator/{id}")]
        public IActionResult getlistingsChifCurator([FromQuery]int id)
        {
            var assigment = _uow.ChiefCuratorAssignmentRepository.Find(p => p.PublicationId.Equals(id)).FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Curation record could not be retrieved or has been submitted");
            }
            return Ok(value: new CrationPublicationsSerilizer(assigment, _uow));
        }
        [HttpGet("getlistingsChifCuratorToGetGuid/{id}")]
        public IActionResult getlistingsChifCuratorToGetGuid([FromQuery]int id)
        {
            var assigment = _uow.CuratorAssignmentRepository.Find(p => p.PublicationId.Equals(id)).FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Curation record could not be retrieved or has been submitted");
            }
            return Ok(value: new CurationRepoDownloadSerilizer(assigment, _uow));
        }
        [HttpPatch("curator/curate/{AssignmentId:int}")]
        public IActionResult SubmitCuration(int AssignmentId, [FromBody]CurationUploadSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var assigment = _uow.CuratorAssignmentRepository.Find(p => !p.Submitted
                                                                  && p.Id == AssignmentId
                                                                  && p.Assignee.Equals(model.UserGuid))
                                                                  .FirstOrDefault();

            if (assigment == null)
            {
                return NotFound("Record could not be retrieved");
            }
            try
            {
                assigment.Notes = model.Notes;
                assigment.FullName = model.FullName;
                if (model.Submitted == "true")
                {
                    assigment.Submitted = true;
                }
                _uow.Complete();
                return Ok(value: CurationAssignments(model.UserGuid, _uow));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("curator/curate/{Id:int}")]
        public IActionResult GetCuration(string userGuid, int Id)
        {
            var assigment = _uow.CuratorAssignmentRepository.Find(p => p.Id.Equals(Id) && !p.Submitted
                                                                && p.Assignee.Equals(userGuid)).FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Curation record could not be retrieved or has been submitted");
            }
            return Ok(value: new CurationRepoDownloadSerilizer(assigment, _uow));
        }
        #endregion

        #region Chief Curator Comments
        [HttpPost("ChiefCuratorComments/{id}")]
        public IActionResult Comments(int publicationId, [FromBody] ChiefCuratorCommentsSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository
                                  .Find(p => p.Id.Equals(publicationId)
                                  && p.ChiefCuratorAssignment.ChiefCuratorGuid.Equals(model.ChiefCuratorGuid))
                                  .FirstOrDefault();
           
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication Not Found in Repository." });
            }
            var exists = _uow.ChiefCuratorCommentRepository.Find(p => p.PublicationId.Equals(model.PublicationId)).Any(); 
            if (exists)
            {
                return BadRequest("Comments for this publication already exists");
            }
            try
            {
                var update = _uow.ChiefCuratorAssignmentRepository
                                  .Find(p => p.PublicationId.Equals(publicationId)).FirstOrDefault();
                update.Submitted = true;
                var comment = new ChiefCuratorComment
                {
                    PublicationId = publication.Id,
                    Notes = model.Notes,
                    ChiefCuratorGuid = model.ChiefCuratorGuid,
                    Recomendation = model.ActionTaken,
                    FullName = model.FullName

                };
                _uow.ChiefCuratorCommentRepository.Add(comment);
                var recommendation = new PublicationStageLog
                {
                    PublicationId = publication.Id,
                    Stage = PublicationStage.PublicationApproval,
                    Notes = model.Notes,
                    CreatedAtUtc = DateTime.UtcNow,
                    Owner = model.ChiefCuratorGuid
                };
                _uow.PublicationStageLogRepository.Add(recommendation);
                _uow.Complete();
                return Ok(value: new { message = "Recommendations Sent To Chief Curator" });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("ChiefCuratorComments/{id}")]
        public IActionResult GetComments([FromQuery]int publicationId)
        {
            try
            {

                var comments = _uow.ChiefCuratorCommentRepository.Find(p =>
                                p.Id.Equals(publicationId)).ToList();
                return Ok(value: comments);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("PrincipalCurator/Comments/{id}")]
        public IActionResult GetCommentsForPrincipal([FromQuery]int publicationId)
        {
            try
            {

                var comments = _uow.CuratorAssignmentRepository.Find(p =>
                                p.PublicationId.Equals(publicationId)
                                && p.Status == true).ToList();
                return Ok(value: comments);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        #endregion
        #region Methods
        private List<CurationDownloadSerializer> CurationAssignments(string userGuid, IUnitOfWork uow)
        {
            var assignments = _uow.CuratorAssignmentRepository.Find(p => p.Assignee.Equals(userGuid) && !p.Submitted);
            var assigmentList = assignments.Any() ?
                assignments.Select(p => new CurationDownloadSerializer(p, uow)).ToList() : new List<CurationDownloadSerializer>();
            return assigmentList;
        }
        #endregion
        #region assign Multiple To Chief Curators
        [HttpPost("selected/assign")]
        public IActionResult AssignSelectedPublications([FromBody] SelectedContentAssignmentSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publications = _uow.PublicationRepository
                                  .Find(p => model.SelectedContent.Contains(p.Id) && !p.FullyAssigned).ToList();
            if (publications == null)
            {
                return NotFound(value: new { message = "Publication could not be retrieved for assignment." });
            }
            var exists = _uow.ChiefCuratorAssignmentRepository.Find(p => model.SelectedContent.Contains(p.PublicationId)).Any();

            if (exists)
            {
                return BadRequest("Content Already Assigned");
            }
            var nextStageList = new List<PublicationStageLog>();
            var assignmentList = new List<ChiefCuratorAssignment>();
            var padLock = new object();

            try
            {
                Parallel.ForEach(publications, (publication, loopState) =>
                {
                    var assignment = new ChiefCuratorAssignment
                    {
                        PublicationId = publication.Id,
                        PrincipalCuratorGuid = model.PrincipalCuratorGuid,
                        ChiefCuratorGuid = model.ChiefCuratorGuid,
                        AssignmetDateUtc = DateTime.UtcNow
                    };
                    var nextStage = new PublicationStageLog
                    {
                        PublicationId = publication.Id,
                        Owner = publication.Owner,
                        Stage = PublicationStage.Curation,
                        ActionTaken = ActionTaken.PublicationMoveToNextStage
                    };

                    _uow.ChiefCuratorAssignmentRepository.Add(assignment);
                    _uow.PublicationStageLogRepository.Add(nextStage);
                    publication.ChiefCuratorAssignmentId = (assignment.Id);
                    assignment.Assigned = true;
                });

                _uow.Complete();

                return Ok(value: new { message = "Content assigned successfully" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
        [HttpGet("publications/{publicationId:int}/curator/comments")]
        public IActionResult ReadCurationCommentsFromCurators(int publicationId)
        {
            var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(publicationId)).FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication record could not be retrieved" });
            }
            return Ok(value: new PublicationDownloadSerilizerToCurators(publication, _uow));
        }
    }

}