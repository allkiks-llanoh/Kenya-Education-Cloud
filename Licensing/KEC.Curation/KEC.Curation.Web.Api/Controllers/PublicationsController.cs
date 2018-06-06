﻿using System;
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

namespace KEC.Curation.Web.Api.Controllers
{
    [AllowCrossSiteJson]
    [Produces("application/json")]
    [Route("api/Publications")]
    public class PublicationsController : Controller
    {
        #region Definitions and Constants
        const string StorageAccountName = "kecpublications";
        const string StorageAccountKey = "cP6MXKU+7YLxE4sF4FPS0ETJ9Q5HzqN4YU+/XCtJgTtKoLpOCaAR4aQc7S5YU8q2/QjCvkGLiynLcQXmBGuifQ==";
        //const string pathToFile = "https://keccuration.file.core.windows.net/publications";
        const string pathToFile = "https://kecpublications.blob.core.windows.net/publicationtest";
        private readonly IUnitOfWork _uow;
        private readonly IHostingEnvironment _env;
        public PublicationsController(IUnitOfWork uow, IHostingEnvironment env)
        {
            _uow = uow;
            _env = env;
        }
        #endregion
        #region Submit Publication
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
        #endregion
        #region Get Publications
        [HttpGet("file/download")]
        public IActionResult DownloadPublication([FromQuery] string url)
        {
            var cred = new StorageCredentials(StorageAccountName, StorageAccountKey);
            var file = new CloudFile(new Uri(url), cred);
            return Ok(value: file);
        }
        [HttpGet("LegalVerification/Legal")]
        public IActionResult PublicationLegal()
        {
            try
            {
                var publication = _uow.PublicationRepository.Find(p => p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.LegalVerification).ToList();
                var publicationList = publication.Any() ?
                publication.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
                return Ok(publicationList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("PaymentVerification/Finance")]
        public IActionResult PublicationFinance()
        {
            try
            {
                var publication = _uow.PublicationRepository.Find(p => p.PublicationStageLogs
                                                        .Max(l => l.Stage) == PublicationStage.PaymentVerification).ToList();
                var publicationList = publication.Any() ?
                publication.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
                return Ok(publicationList);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
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
        [HttpGet("allpublications")]
        public IActionResult AllPublications()
        {
            var assignments = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals(PublicationStage.NewPublication));
            var assignmentList = assignments.Any() ?
                assignments.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(assignmentList);
        }
        #endregion

        #region Patch Process Publication
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
            var maxStage = _uow.PublicationStageLogRepository.Find(p => p.PublicationId == publication.Id).Max(p => p.Stage);
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
        #endregion
        #region Patch Reject Publication
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
        #endregion
    }
}