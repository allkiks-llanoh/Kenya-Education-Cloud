using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{
    [EnableCors ("*")]
    [Produces("application/json")]
    [Route("api/Publications")]
    public class PublicationsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IHostingEnvironment _env;

        public PublicationsController(IUnitOfWork uow, IHostingEnvironment env)
        {
            _uow = uow;
            _env = env;
        }
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromForm]PublicationUploadSerilizer model)
        {
            var invaliExtension = Path.GetExtension(model.PublicationFile.FileName).ToLower().Equals(".exe");

            if (invaliExtension == true)
            {
                ModelState.AddModelError("File", ".EXE File Extensions are not Permited");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
           
            try
            {

                var filePath = new System.Uri( $"{_env.ContentRootPath}\\Publications\\{DateTime.Now.ToString("yyyyMMddHHmmss")}{model.PublicationFile.FileName}");
                var converted = filePath.AbsolutePath;
                var publication = new Publication
                {
                    AuthorName = model.AuthorName,
                    PublisherName = model.PublisherName,
                    SubjectId = model.SubjectId,
                    LevelId = model.LevelId,
                    CompletionDate = model.CompletionDate.Value,
                    Description = model.Description,
                    ISBNNumber = model.ISBNNumber,
                    Price = model.Price.GetValueOrDefault(),
                    Title = model.Title,
                    MimeType = model.PublicationFile.ContentType,
                    Url = converted,
                    KICDNumber = _uow.PublicationRepository
                                      .GetKICDNUmber(_uow.PublicationRepository.GetAll().ToList()),
                    CreatedTimeUtc = DateTime.UtcNow,
                    ModifiedTimeUtc = DateTime.UtcNow

                };
                publication.PublicationStageLogs.Add(new PublicationStageLog
                {
                    Stage = PublicationStage.NewPublication,
                    Owner = model.UserGuid,
                    CreatedAtUtc = DateTime.UtcNow,
                    ActionTaken = ActionTaken.PublicationSubmitted

                });
                _uow.PublicationRepository.Add(publication);
                _uow.Complete();
                _uow.PublicationRepository.ProcessToTheNextStage(publication);
                using (var memoryStream = new MemoryStream())
                {
                    await model.PublicationFile.CopyToAsync(memoryStream);
                    var fileStream = new FileStream(converted, FileMode.CreateNew, FileAccess.ReadWrite);
                    memoryStream.WriteTo(fileStream);
                }
                return Ok(value: "Publication submitted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpGet("{subjectId:int}/{stage}/get")]
        public IActionResult Publication(PublicationStage stage, int subjectId)
        {
            try
            {

                var publication = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals
                             (PublicationStage.LegalVerification)).ToList();
                return Ok(value: publication);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{subjectId:int}/{stage}")]
        public IActionResult PublicationsByStage(PublicationStage stage, int subjectId)
        {
            try
            {
                
                var stageLevel = (int)stage;
                var publicationIds = _uow.PublicationRepository
                                         .Find(p => p.PublicationStageLogs.Count == stageLevel
                                               && !p.PublicationStageLogs.Any(l => l.Stage > stage)
                                               
                                               && !p.PublicationStageLogs
                                              
                                                   .Any(l => l.ActionTaken == ActionTaken.PublicationRejected)
                                               && p.Subject.Id.Equals(subjectId))
                                         .Select(p => p.Id);
                var publications = _uow.PublicationRepository.Find(p => publicationIds.Contains(p.Id));
                var publicationList = publications.Any() ?
                            publications.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
                return Ok(value: publicationList);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPatch("process")]
        public IActionResult ProcessPublication([FromBody]PublicationProcessingSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository.Find(p => p.KICDNumber.Equals(model.KICDNumber)).FirstOrDefault();

            if (publication == null)
            {
                return NotFound(value: $"Publication {model.KICDNumber} could not be located");
            }
            var maxStage = _uow.PublicationStageLogRepository.Find(p=> p.PublicationId==publication.Id).Max(p => p.Stage);
            var publicationLog = _uow.PublicationStageLogRepository.Find(p => p.Stage == model.Stage
                                                            && p.Stage == maxStage
                                                            && p.PublicationId.Equals(publication.Id)
                                                            && p.Owner == null && p.ActionTaken == null).FirstOrDefault();

            if (publicationLog == null)
            {
                return BadRequest(error: $"Publication {model.KICDNumber} has already been processed for the stage");
            }
            if (model.Stage == PublicationStage.Curation && 
                !_uow.PublicationRepository.CanProcessCurationPublication(publication))
            {
                return BadRequest(error: $"Publication {model.KICDNumber} has pending curation notes");
            }
            try
            {
                publicationLog.Owner = model.UserGuid;
                publicationLog.ActionTaken = model.ActionTaken;
                _uow.Complete();
                _uow.PublicationRepository.ProcessToTheNextStage(publication);
                return Ok(value: $"Publication {model.KICDNumber} processed successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPatch("reject")]
        public IActionResult ProcessRejection([FromBody]PublicationProcessingSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository.Find(p => p.KICDNumber.Equals(model.KICDNumber)).FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: $"Publication {model.KICDNumber} could not be located");
            }
            var maxStage = _uow.PublicationStageLogRepository.Find(p => p.PublicationId == publication.Id).Max(p => p.Stage);
            var publicationLog = _uow.PublicationStageLogRepository.Find(p => p.Stage == model.Stage
                                                            && p.Stage == maxStage
                                                            && p.PublicationId.Equals(publication.Id)
                                                            && p.Owner == null && p.ActionTaken == null).FirstOrDefault();

            if (publicationLog == null)
            {
                return BadRequest(error: $"Publication {model.KICDNumber} has already been processed for the stage");
            }
            
            try
            {
               
                publicationLog.ActionTaken = model.ActionTaken;
                _uow.Complete();
              
                return Ok(value: $"Publication {model.KICDNumber} has been Rejected");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpGet("allpublications")]
        public IActionResult AllPublications()
        {
            var assignments = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals(PublicationStage.NewPublication));
            var assignmentList = assignments.Any() ?
                assignments.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(assignmentList);
        }
    }
}