using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Cors;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{

    [AllowCrossSiteJson]
   
  
    [Produces("application/json")]
    [Route("api/PrincipalCurator")]
    public class PrincipalCuratorController : Controller
    {

        private readonly IUnitOfWork _uow;


        public PrincipalCuratorController(IUnitOfWork uow)
        {
            _uow = uow;

        }

        [HttpGet]
        public IActionResult Principal()
        {

            var publicatons = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals
                              (PublicationStage.PrincipalCurator));
            var publicationList = publicatons.Any() ?
                publicatons.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList);


        }
        [HttpGet("publications/assignedtochiefs")]
        public IActionResult ToCuration()
        {

            var publicatons = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals
                              (PublicationStage.Curation));
            var publicationList = publicatons.Any() ?
                publicatons.Select(p => new PrincipalCuratorDownloadSerilizer(p, _uow)).ToList() : new List<PrincipalCuratorDownloadSerilizer>();
            return Ok(value: publicationList);


        }
        [HttpGet("Assigned")]
        public IActionResult Assigned(string principalCuratorGuid)
        {

            var publications = _uow.PublicationRepository.Find(p => p.ChiefCuratorAssignment.
                                       PrincipalCuratorGuid.Equals(principalCuratorGuid)
                                       && p.PublicationStageLogs.Max(l => l.Stage)
                                       == PublicationStage.Curation);
            var publicationList = publications.Any() ?
                publications.Select(p => new PrincipalCuratorDownloadSerilizer(p, _uow)).ToList() : new List<PrincipalCuratorDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        [HttpGet("withcomments")]
        public IActionResult WithComments(string principalCuratorGuid)
        {

            var publications = _uow.PublicationRepository.Find(p => p.ChiefCuratorAssignment.
                                       PrincipalCuratorGuid.Equals(principalCuratorGuid)
                                        && p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.PublicationApproval);
            var publicationList = publications.Any() ?
                publications.Select(p => new PrincipalCuratorDownloadSerilizer(p, _uow)).ToList() : new List<PrincipalCuratorDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        [HttpGet("Curated")]
        public IActionResult CuratedPublications([FromQuery] string principalCuratorGuid, int publicationId)
        {

            try
            {

                var publications = _uow.PublicationRepository.Find(p =>
                                   p.Id.Equals(publicationId)
                                   && p.ChiefCuratorAssignment.PrincipalCuratorGuid.Equals(principalCuratorGuid)
                                    && p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.PublicationApproval);
                return Ok(value: publications.ToList());

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }
        [HttpGet("{stage}")]
        public IActionResult PublicationsByStage(PublicationStage stage)
        {
            try
            {

                var stageLevel = (int)stage;
                var publicationIds = _uow.PublicationRepository
                                         .Find(p => p.PublicationStageLogs.Count == stageLevel
                                               && !p.PublicationStageLogs.Any(l => l.Stage > stage)

                                               && !p.PublicationStageLogs

                                                   .Any(l => l.ActionTaken == ActionTaken.PublicationRejected))
                                               .Select(p => p.Id);

                var publications = _uow.PublicationRepository.Find(p => publicationIds.Contains(p.Id));
                var publicationList = publications.Any() ?
                            publications.Select(p => new PrincipalCuratorDownloadSerilizer(p, _uow)).ToList() : new List<PrincipalCuratorDownloadSerilizer>();
                return Ok(value: publicationList);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
        [HttpGet("Reverse/{stage}")]
        public IActionResult PublicationsToReverse(PublicationStage stage)
        {
            try
            {

                var stageLevel = (int)stage;
                var publicationIds = _uow.PublicationRepository
                                         .Find(p => p.PublicationStageLogs.Count == stageLevel
                                               && !p.PublicationStageLogs.Any(l => l.Stage > stage)

                                               && !p.PublicationStageLogs

                                                   .Any(l => l.ActionTaken == ActionTaken.PublicationRejected))
                                               .Select(p => p.Id);

                var publications = _uow.PublicationRepository.Find(p => publicationIds.Contains(p.Id));
                var publicationList = publications.Any() ?
                            publications.Select(p => new PrincipalCuratorDownloadSerilizer(p, _uow)).ToList() : new List<PrincipalCuratorDownloadSerilizer>();
                return Ok(value: publicationList);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
        // POST: api/PrincipalCurator/publication/publicationId/assign
        [HttpPost("publication/{publicationId:int}/assign")]
        public IActionResult Assign(int publicationId, [FromBody]ChiefCuratorAssignmentSerializer model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository.Find(p => p.KICDNumber.Equals(model.KICDNumber)).FirstOrDefault();

            if (publication == null)
            {
                return NotFound(value: $"Publication {model.KICDNumber} could not be found");
            }
            var maxStage = _uow.PublicationStageLogRepository.Find(p => p.PublicationId == publication.Id).Max(p => p.Stage);
            var publicationLog = _uow.PublicationStageLogRepository.Find(p => p.Stage == model.Stage
                                                            && p.Stage == maxStage
                                                            && p.PublicationId.Equals(publication.Id)
                                                            && p.Owner == null && p.ActionTaken == null).FirstOrDefault();

            if (publicationLog == null)
            {
                return BadRequest(error: $"Publication {model.KICDNumber} has already been processed for curation");
            }

            try
            {

                var asignment = new ChiefCuratorAssignment
                {
                    PublicationId = publication.Id,
                    PrincipalCuratorGuid = model.PrincipalCuratorGuid,
                    ChiefCuratorGuid = model.ChiefCuratorGuid,
                    AssignmetDateUtc = DateTime.UtcNow,
                    

                }; 
                var nextStage = new PublicationStageLog
                {
                    PublicationId = publication.Id,
                    Owner = publication.Owner,
                    Notes = model.Notes,
                    Stage = PublicationStage.Curation,
                    ActionTaken = model.ActionTaken
                };
               
                _uow.ChiefCuratorAssignmentRepository.Add(asignment);
                _uow.PublicationStageLogRepository.Add(nextStage);
                publication.ChiefCuratorAssignmentId =(asignment.Id);
                _uow.Complete();

                return Ok(value: $"Publication {model.KICDNumber} moved to curation");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPost("PrincipalCuratorComments/{id}")]
        public IActionResult Comments(int publicationId, [FromBody] PrincipalCuratorCommentsSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository
                                  .Find(p => p.Id.Equals(publicationId)
                                  && p.ChiefCuratorAssignment.PrincipalCuratorGuid.Equals(model.PrincipalCuratorGuid)
                                   && p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.PublicationApproval)
                                  .FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication Not Found in Repository." });
            }

            try
            {


                var comment = new PrincipalCuratorComment
                {
                    PublicationId = publication.Id,
                    Notes = model.Notes,
                    PrincipalCuratorGuid = model.PrincipalCuratorGuid,


                };
                _uow.PrincipalCuratorCommentRepository.Add(comment);
                var recommendation = new PublicationStageLog
                {
                    PublicationId = publication.Id,
                   
                    Notes = model.Notes,
                    CreatedAtUtc = DateTime.UtcNow,
                    Owner = model.PrincipalCuratorGuid,
                    ActionTaken = model.ActionTaken
                };
                _uow.PublicationStageLogRepository.Add(recommendation);
              
                _uow.Complete();
                _uow.PublicationRepository.ProcessToTheNextStage(publication);
                return Ok(value: new { message = "Recommendations Sent To Curation Managers" });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPatch("update/chiefcuratorcomments/{id}")]
        public IActionResult UpdateChiefCurationComments(int publicationId, [FromBody]ChiefFlagSubmittedSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var assigment = _uow.ChiefCuratorAssignmentRepository.Find(p => p.Submitted==false
                                                                  && p.PublicationId.Equals(model.publicationId)
                                                                  && p.PrincipalCuratorGuid.Equals(model.UserGuid))
                                                                  .FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Record could not be retrieved");
            }
            try
            {
               
                assigment.Submitted = true;
                _uow.Complete();
                return Ok(value: new { message = "Curation Fully Submitted" });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("publication/assignment/{id}")]
        public IActionResult DeleteUnAssignedPublication(int Id, [FromBody]string principalCuratorGuid)
        {
            try
            {
                var assignment = _uow.ChiefCuratorAssignmentRepository.Find(p => p.PrincipalCuratorGuid.Equals(principalCuratorGuid) && p.Id.Equals(Id)).FirstOrDefault();
                if (assignment == null)
                {
                    return BadRequest(error: new { message = "Chief Curator assigment cannot be deleted" });
                }
                _uow.ChiefCuratorAssignmentRepository.Remove(assignment);
                _uow.Complete();
                return Ok(value: new { message = "Chief Curator assignment deleted successfully" });
            }
            catch (Exception)
            {
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
