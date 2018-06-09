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
using System.Net;
using System.IO.Compression;

namespace KEC.Curation.Web.Api.Controllers
{

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
        [RequestSizeLimit(3_000_000_000)]
        public async Task<IActionResult> Submit([FromForm]PublicationUploadSerilizer model)
        {
            #region Extensions Check
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var invaliExtension = Path.GetExtension(model.PublicationFile.FileName).ToLower().Equals(".exe");
            var htmlFile = Path.GetExtension(model.PublicationFile.FileName).ToLower().Equals(".zip");

            if (invaliExtension == true)
            {
                ModelState.AddModelError("File", ".EXE File Extensions are not Permited");
            }
            #endregion
            #region Blobs Connection 
            var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), false);
            //Blob starts here
            var blob = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blob.GetContainerReference("publications");
            CloudBlockBlob blobs = container.GetBlockBlobReference($"{DateTime.Now.ToString("yyyyMMddHHmmss")}{model.PublicationFile.FileName}");
            var filePath = $"{pathToFile}/c{model.PublicationFile.FileName}";
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Path = filePath;
            var ul = uriBuilder.Uri;
            #endregion
            //if (htmlFile == true)
            //{

            //    //BackgroundJob.Enqueue(() =>  WorkOnPublications(model));
            //   // return Ok(value:"Success");
            //}
            #region For .ZIP Files
            if (htmlFile == true)
            {
                try
                {

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

                    var uri = publication.Url;
                    var wc = new WebClient();

                    var nstream = wc.OpenRead(uri);
                    var uploads = Path.Combine(_env.ContentRootPath, "uploads");
                    string lastFolderName = Path.GetFileNameWithoutExtension(uri);
                    string curationUrl = string.Empty;

                    using (ZipArchive archive = new ZipArchive(nstream))
                    {

                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            string completeFileName = Path.Combine(uploads, lastFolderName, entry.FullName);
                            if (!string.IsNullOrEmpty(Path.GetExtension(entry.FullName)))

                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                                entry.ExtractToFile(completeFileName, true);
                            }
                            else
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                                //entry.ExtractToFile(completeFileName, true);
                                continue;
                            }

                            string returnedIndex = "Index.html";
                            var returnedUrl = $"{"https://publisherapi.kec.ac.ke/staticfiles/"}{lastFolderName}/{returnedIndex}";
                            curationUrl = returnedUrl;
                        }

                    }

                    publication.CutationUrl = curationUrl;
                    _uow.Complete();
                    return Ok(value: "Publication submitted successfully");
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }

            }

            #endregion

            #region For .Epub Files
            try
            {

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
                    Owner = model.UserGuid,
                    CutationUrl = blobs.StorageUri.PrimaryUri.ToString(),
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

                _uow.Complete();
                return Ok(value: "Publication submitted successfully");
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            #endregion

        }
        #region Backgound Task
        //public async Task WorkOnPublications([FromForm]PublicationUploadSerilizer model)
        //{
        //    var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), false);
        //    //Blob starts here
        //    var blob = storageAccount.CreateCloudBlobClient();
        //    CloudBlobContainer container = blob.GetContainerReference("publications");
        //    CloudBlockBlob blobs = container.GetBlockBlobReference($"{model.PublicationFile.FileName}");
        //    var filePath = $"{pathToFile}/{DateTime.Now.ToString("yyyyMMddHHmmss")}{model.PublicationFile.FileName}";
        //    UriBuilder uriBuilder = new UriBuilder();
        //    uriBuilder.Path = filePath;
        //    var ul = uriBuilder.Uri;

        //    try
        //    {

        //        var publication = new Publication
        //        {
        //            AuthorName = model.AuthorName,
        //            PublisherName = model.PublisherName,
        //            SubjectId = model.SubjectId,
        //            LevelId = model.LevelId,
        //            CompletionDate = model.CompletionDate.Value,
        //            Description = model.Description,
        //            ISBNNumber = model.ISBNNumber,
        //            Price = model.Price.GetValueOrDefault(),
        //            Title = model.Title,
        //            MimeType = model.PublicationFile.ContentType,
        //            Url = blobs.StorageUri.PrimaryUri.ToString(),
        //            KICDNumber = _uow.PublicationRepository
        //                              .GetKICDNUmber(_uow.PublicationRepository.GetAll().ToList()),
        //            CreatedTimeUtc = DateTime.Now.Date,
        //            ModifiedTimeUtc = DateTime.Now.Date,
        //            Owner = model.UserGuid
        //        };
        //        publication.PublicationStageLogs.Add(new PublicationStageLog
        //        {
        //            Stage = PublicationStage.NewPublication,
        //            Owner = model.UserGuid,
        //            CreatedAtUtc = DateTime.UtcNow,
        //            ActionTaken = ActionTaken.PublicationSubmitted
        //        });
        //        _uow.PublicationRepository.Add(publication);
        //        _uow.Complete();
        //        _uow.PublicationRepository.ProcessToTheNextStage(publication);

        //        var tempPath = Path.GetTempFileName();
        //        using (var stream = new FileStream(tempPath, FileMode.Create))
        //        {
        //            await model.PublicationFile.CopyToAsync(stream);
        //        }
        //        blobs.Properties.ContentType = $"{model.PublicationFile.ContentType}";

        //        using (var stream = new FileStream(tempPath, FileMode.Open, FileAccess.ReadWrite))
        //        {
        //            await blobs.UploadFromStreamAsync(stream);
        //        }

        //        var uri = publication.Url;
        //        var wc = new WebClient();

        //        var nstream = wc.OpenRead(uri);
        //        var uploads = Path.Combine(_env.ContentRootPath, "uploads");
        //        string lastFolderName = Path.GetFileNameWithoutExtension(uri);
        //        string curationUrl = string.Empty;
        //        using (var stream = new FileStream(tempPath, FileMode.Open, FileAccess.ReadWrite))
        //        {
        //            using (ZipArchive archive = new ZipArchive(stream))
        //            {

        //                foreach (ZipArchiveEntry entry in archive.Entries)
        //                {
        //                    string completeFileName = Path.Combine(uploads, lastFolderName, entry.FullName);
        //                    if (!string.IsNullOrEmpty(Path.GetExtension(entry.FullName)))

        //                    {
        //                        Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
        //                        entry.ExtractToFile(completeFileName, true);
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
        //                    }

        //                    string returnedIndex = "Index.html";
        //                    var returnedUrl = $"{"https://publisherapi-d.kec.ac.ke/staticfiles/"}{lastFolderName}/{returnedIndex}";
        //                    curationUrl = returnedUrl;
        //                }

        //            }
        //        }
        //        publication.CutationUrl = curationUrl;
        //        _uow.Complete();

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.GetBaseException();

        //    }
        //}
        #endregion
        #endregion
        #region PUblishers Count
        [HttpGet("count/approved/publisher/{guid}")]
        public IActionResult CountApprovedPublisher(string guid)
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => p.Approved && p.Owner.Equals(guid)).Count();
            return Ok(value: publicationsCount);
        }
        [HttpGet("count/rejected/publisher/{guid}")]
        public IActionResult CountRejectedPublisher(string guid)
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => p.Rejected && p.Owner.Equals(guid)).Count();
            return Ok(value: publicationsCount);
        }
        [HttpGet("count/all/publisher/{guid}")]
        public IActionResult CountAllPublisher(string guid)
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => p.Owner.Equals(guid)).Count();
            return Ok(value: publicationsCount);
        }
        #endregion
        #region Publishers Publications
        [HttpGet("get/all/publisher/{guid}")]
        public IActionResult GetAll(string guid)
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => p.Owner.Equals(guid)).ToList();
            return Ok(value: publicationsCount);
        }
        [HttpGet("get/approved/publisher/{guid}")]
        public IActionResult GetApproved(string guid)
        {
            var publicatons = _uow.PublicationRepository.Find(p => p.Approved && p.Owner.Equals(guid));
            var publicationList = publicatons.Any() ?
                publicatons.Select(p => new PublisherContentDownloadSerilizer(p, _uow)).ToList() : new List<PublisherContentDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        [HttpGet("get/rejected/publisher/{guid}")]
        public IActionResult GetRejected(string guid)
        {
            var publicatons = _uow.PublicationRepository.Find(p => p.Rejected && p.Owner.Equals(guid));
            var publicationList = publicatons.Any() ?
                publicatons.Select(p => new PublisherContentDownloadSerilizer(p, _uow)).ToList() : new List<PublisherContentDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        [HttpGet("get/pending/publisher/{guid}")]
        public IActionResult GetPending(string guid)
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => !p.Rejected && p.Owner.Equals(guid) && !p.Approved && p.PublicationStageLogs.Max(l => l.Stage)
                                       == PublicationStage.IssueOfCertificate).ToList();
            return Ok(value: publicationsCount);
        }
        #endregion
        #region Get Publications

        [HttpGet("get/publications/{Id}")]
        public IActionResult GetAllPublicationsById(int Id)
        {
            var publications = _uow.PublicationRepository.Find(p => p.Id.Equals(Id));
            var publicationList = publications.Any() ?
               publications.Select(p => new PublicationDownloadSerilizerToCurators(p, _uow)).ToList() : new List<PublicationDownloadSerilizerToCurators>();
            return Ok(value: publicationList);
        }
        [HttpGet("get/publications")]
        public IActionResult GetAllPublications()
        {
            var publications = _uow.PublicationRepository.GetAll().ToList();
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        [HttpGet("get/approved")]
        public IActionResult GetApproved()
        {
            var publications = _uow.PublicationRepository.Find(p => p.Approved).ToList();
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        [HttpGet("get/approved/{Id}")]
        public IActionResult GetApprovedById(int Id)
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => p.Approved && p.Id.Equals(Id)).FirstOrDefault();
            return Ok(value: publicationsCount);
        }
        [HttpGet("get/rejected")]
        public IActionResult GetRejected()
        {
            var publications = _uow.PublicationRepository.Find(p => p.Rejected).ToList();
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        [HttpGet("get/rejected/{Id}")]
        public IActionResult GetRejectedById(int Id)
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => p.Rejected && p.Id.Equals(Id)).FirstOrDefault();
            return Ok(value: publicationsCount);
        }
        [HttpGet("get/pending")]
        public IActionResult GetPending()
        {
            var publications = _uow.PublicationRepository.Find(p => !p.Rejected && !p.Approved && p.PublicationStageLogs.Max(l => l.Stage)
                                       == PublicationStage.IssueOfCertificate).ToList();
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList);
        }
        //Pending Publications By Id
        [HttpGet("get/pending/{id}")]
        public IActionResult GetPendingById(int Id)
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => !p.Rejected && !p.Approved && p.PublicationStageLogs.Max(l => l.Stage)
                                       == PublicationStage.IssueOfCertificate
                                       && p.Id.Equals(Id)).FirstOrDefault();
            return Ok(value: publicationsCount);
        }
        #endregion
    }
}