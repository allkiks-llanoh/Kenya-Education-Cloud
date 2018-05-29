using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using Newtonsoft.Json.Linq;
using Aspose.Pdf;

namespace KEC.Curation.Publishers.Web.Api.Controllers
{
    [AllowCrossSiteJson]
    [Produces("application/json")]
    [Route("api/Publishers")]
    public class PublishersController : Controller
    {
        #region Definitions and Constants
        const string StorageAccountName = "kecpublications";
        const string StorageAccountKey = "MKMdAUoU2vUWIRSMSR5UXnu7hZ9omXWFXSglCHowJTn3oyOeycxCGSgSQ4huvihn9a0DUObjvvNBd06PfHw2Dg==";
        const string pathToFile = "https://kecpublications.blob.core.windows.net/publications/";
        private readonly IUnitOfWork _uow;
        private readonly IHostingEnvironment _env;
        public PublishersController(IUnitOfWork uow, IHostingEnvironment env)
        {
            _uow = uow;
            _env = env;
        }
        #endregion
        [HttpGet("count/approved")]
        public IActionResult ApprovedCount([FromBody]PublicationCountSerilizer model)
        {
            var guid = model.UserGuid;
            var stage = model.Stage;

            try
            {
                var publicationsCount = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals
                                                               (ActionTaken.PublicationApproved)
                                                                && p.Owner.Equals(guid)).Count();
                return Ok(value: publicationsCount);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("count/partial")]
        public IActionResult PartialCount([FromBody]PublicationCountSerilizer model)
        {
            var guid = model.UserGuid;
            var stage = model.Stage;

            try
            {
                var publicationsCount = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals
                                                               (ActionTaken.PublicationConditionalApproval)
                                                                && p.Owner.Equals(guid)).Count();
                return Ok(value: publicationsCount);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("count/rejected")]
        public IActionResult RejectedCount([FromBody]PublicationCountSerilizer model)
        {
            var guid = model.UserGuid;
            var stage = model.Stage;

            try
            {
                var publicationsCount = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals
                                                               (ActionTaken.PublicationRejected)
                                                               && p.Owner.Equals(guid)).Count();
                return Ok(value: publicationsCount);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
                                      conditionalapproved.Select(p => new PublicationDownloadSerilizer
                                      (p, _uow)).ToList()
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
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var invaliExtension = Path.GetExtension(model.PublicationFile.FileName).ToLower().Equals(".exe");

            if (invaliExtension == true)
            {
                ModelState.AddModelError("File", ".EXE File Extensions are not Permited");
            }
            var fileConverted = Path.GetExtension(model.PublicationFile.FileName).ToLower().Equals(".epub");

            if (fileConverted == true)
            {
                try
                {
                  
                    EpubLoadOptions epubload = new EpubLoadOptions();
                  
            
                    Aspose.Pdf.Document pdf = new Aspose.Pdf.Document($"{model.PublicationFile.FileName}", epubload);

                    var document = ($"{model.PublicationFile}{DateTime.Now.ToString("yyyyMMddHHmmss")}{".pdf"}");
                    var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), false);
                    //Blob starts here
                    var blob = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blob.GetContainerReference("publications");
                    CloudBlockBlob blobs = container.GetBlockBlobReference($"{model.PublicationFile.FileName}");
                    var filePath = $"{pathToFile}/{DateTime.Now.ToString("yyyyMMddHHmmss")}{model.PublicationFile.FileName}";
                    UriBuilder uriBuilder = new UriBuilder();
                    uriBuilder.Path = filePath;
                    var ul = uriBuilder.Uri;
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
                        Url = $"{pathToFile}{document}",
                        KICDNumber = _uow.PublicationRepository
                                          .GetKICDNUmber(_uow.PublicationRepository.GetAll().ToList()),
                        CreatedTimeUtc = DateTime.Now.Date,
                        ModifiedTimeUtc = DateTime.Now.Date,
                        Owner = model.UserGuid
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
                    var tempPath = Path.GetTempFileName();
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        await model.PublicationFile.CopyToAsync(stream);
                    }
                    blobs.Properties.ContentType = $"{model.PublicationFile.ContentType}";

                    using (var stream = new FileStream(tempPath, FileMode.Open, FileAccess.ReadWrite))
                    {
                        await blobs.UploadFromStreamAsync(stream);
                    }
                    return Ok(value: "Publication submitted successfully");
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }
            }

            else
            {
                try
                {
                    var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), false);
                    //Blob starts here
                    var blob = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blob.GetContainerReference("publications");
                    CloudBlockBlob blobs = container.GetBlockBlobReference($"{model.PublicationFile.FileName}");
                    var filePath = $"{pathToFile}/{DateTime.Now.ToString("yyyyMMddHHmmss")}{model.PublicationFile.FileName}";
                    UriBuilder uriBuilder = new UriBuilder();
                    uriBuilder.Path = filePath;
                    var ul = uriBuilder.Uri;
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
                        Url = blobs.StorageUri.PrimaryUri.ToString(),
                        KICDNumber = _uow.PublicationRepository
                                          .GetKICDNUmber(_uow.PublicationRepository.GetAll().ToList()),
                        CreatedTimeUtc = DateTime.Now.Date,
                        ModifiedTimeUtc = DateTime.Now.Date,
                        Owner = model.UserGuid
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
                    var tempPath = Path.GetTempFileName();
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        await model.PublicationFile.CopyToAsync(stream);
                    }
                    blobs.Properties.ContentType = $"{model.PublicationFile.ContentType}";

                    using (var stream = new FileStream(tempPath, FileMode.Open, FileAccess.ReadWrite))
                    {
                        await blobs.UploadFromStreamAsync(stream);
                    }
                    return Ok(value: "Publication submitted successfully");
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }
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
