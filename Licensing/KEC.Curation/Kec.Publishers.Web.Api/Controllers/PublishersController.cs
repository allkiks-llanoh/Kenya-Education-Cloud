using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Publishers.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Publishers")]
    public class PublishersController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IHostingEnvironment _env;

        public PublishersController(IUnitOfWork uow, IHostingEnvironment env)
        {
            _uow = uow;
            _env = env;
        }
        [HttpGet("approved")]
        public IActionResult PublicationsByStage(string guid)
        {
            try
            {

                 var approved = _uow.PublicationRepository.Find(p => p.PublicationStageLogs
                 .Equals(ActionTaken.PublicationApproved)
                 && p.Owner.Equals(guid)).ToList();
                 var publicationList = approved.Any() ?
                            approved.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() 
                            : new List<PublicationDownloadSerilizer>();
                return Ok(value: publicationList);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("conditional")]
        public IActionResult Conditional(string guid)
        {
            try
            {

                var conditionalapproved = _uow.PublicationRepository.Find(p => p.PublicationStageLogs
                .Equals(ActionTaken.PublicationConditionalApproval)
                && p.Owner.Equals(guid)).ToList();
                var publicationList = conditionalapproved.Any() ?
                           conditionalapproved.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList()
                           : new List<PublicationDownloadSerilizer>();
                return Ok(value: publicationList);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("rejected")]
        public IActionResult Rejected(string guid)
        {
            try
            {
                
                var rejected = _uow.PublicationRepository.Find(p => p.PublicationStageLogs
                .Equals(ActionTaken.PublicationRejected)
                && p.Owner.Equals(guid)).ToList();
                var publicationList = rejected.Any() ?
                           rejected.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList()
                           : new List<PublicationDownloadSerilizer>();
                return Ok(value: publicationList);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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

                var filePath = new System.Uri( $"{_env.ContentRootPath}/Publications/{DateTime.Now.ToString("yyyyMMddHHmmss")}{model.PublicationFile.FileName}");
                var converted = filePath.AbsoluteUri;
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

        // PUT: api/Publishers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
