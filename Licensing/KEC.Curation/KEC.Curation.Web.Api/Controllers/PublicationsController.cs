using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{
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
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            try
            {
                var filePath = $"{_env.ContentRootPath}/Publications/{DateTime.Now.ToString("yyyyMMddHHmmss")}{model.PublicationFile.FileName}";
                var publication = new Publication
                {
                    AuthorName = model.AuthorName,
                    PublisherName = model.PublisherName,
                    SubjectId = model.SubjectId.Value,
                    LevelId = model.LevelId.Value,
                    CompletionDate = model.CompletionDate.Value,
                    Description = model.Description,
                    ISBNNumber = model.ISBNNumber,
                    Title = model.Title,
                    MimeType = model.PublicationFile.ContentType,
                    Url = filePath,
                    KICDNumber = _uow.PublicationRepository
                                      .GetKICDNUmber(_uow.PublicationRepository.GetAll().ToList()),
                    CreatedTimeUtc = DateTime.UtcNow,
                    ModifiedTimeUtc = DateTime.UtcNow
                };
                _uow.PublicationRepository.Add(publication);
                _uow.Complete();
                using (var memoryStream = new MemoryStream())
                {
                    await model.PublicationFile.CopyToAsync(memoryStream);
                    var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite);
                    memoryStream.WriteTo(fileStream);
                }
                return Ok(value: "Publication submitted successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}