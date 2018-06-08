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
    [Route("api/CurationManagers")]
    public class CurationManagersController : Controller
    {
        private readonly IUnitOfWork _uow;
        public CurationManagersController(IUnitOfWork uow)
        {
            _uow = uow;
        }
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
        
        #region Get Counts
        [HttpGet("count/publications")]
        public IActionResult CountPublications()
        {
            var publicationsCount = _uow.PublicationRepository.GetAll().Count();
            return Ok(value: publicationsCount);
        }
        [HttpGet("count/approved")]
        public IActionResult CountApproved()
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => p.Approved).Count();
            return Ok(value: publicationsCount);
        }
        [HttpGet("count/rejected")]
        public IActionResult CountRejected()
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => p.Rejected).Count();
            return Ok(value: publicationsCount);
        }
        [HttpGet("count/pending")]
        public IActionResult CountPending()
        {
            var publicationsCount = _uow.PublicationRepository.Find(p => !p.Rejected && !p.Approved && p.PublicationStageLogs.Max(l => l.Stage)
                                       == PublicationStage.PublicationApproval).Count();
            return Ok(value: publicationsCount);
        }
        #endregion
        #region Get Comments
        [HttpGet("getcomments")]
        public IActionResult GetComments(int publicationId)
        {
            var curationComments = _uow.CurationManagersCommentRepository.Find(p => p.PublicationId.Equals(publicationId));
            var curationCommentsList = curationComments.Any() ?
                curationComments.Select(p => new GetComments(p, _uow)).ToList() : new List<GetComments>();
            return Ok(value: curationCommentsList);
        }
        [HttpGet("getcomments/fromcuration")]
        public IActionResult GetCommentsFromCuration(int publicationId)
        {
            var curationComments = _uow.PublicationRepository.Find(p => p.Id.Equals(publicationId));
            var curationCommentsList = curationComments.Any() ?
                curationComments.Select(p => new GetCommentsFromCurationSerilizer(p, _uow)).ToList() : new List<GetCommentsFromCurationSerilizer>();
            return Ok(value: curationCommentsList);
        }
        #endregion
        #region Create Comment
        [HttpPost]
        public IActionResult CreateComment([FromBody] CurationManagersSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var licenceString = string.Empty;
            var categoryString = string.Empty;
            var subjectString = string.Empty;


            var assigment = _uow.PublicationRepository.Find(p => p.Id.Equals(model.PublicationId)
                                                                     && p.PublicationStageLogs.Max(l => l.Stage)
                                                                     == PublicationStage.IssueOfCertificate)
                                                                    .FirstOrDefault();
            

            if (assigment == null)
            {
                return NotFound("Record could not be retrieved");
            }
            var subject = _uow.SubjectRepository.Find(p => p.Id.Equals(assigment.SubjectId)).FirstOrDefault();
            #region Get Licence TYpe
            if (assigment.Price == 0) { licenceString = "OL"; }
            if (assigment.Price > 0) { licenceString = "CL"; }
            #endregion
            #region Get Category Type
            if (subject.SubjectTypeId == 1) { categoryString = "CM"; }
            if (subject.SubjectTypeId == 2) { categoryString = "SM"; }
            if (subject.SubjectTypeId == 3) { categoryString = "OR"; }
            if (subject.SubjectTypeId == 4) { categoryString = "MA"; }
            if (subject.SubjectTypeId == 5) { categoryString = "EP"; }
            #endregion
            #region Get Subject
            if (subject.Id == 1) { subjectString = "01"; }
            if (subject.Id == 2) { subjectString = "02"; }
            if (subject.Id == 3) { subjectString = "03"; }
            if (subject.Id == 4) { subjectString = "04"; }
            if (subject.Id == 5) { subjectString = "05"; }
            if (subject.Id == 6) { subjectString = "06"; }
            if (subject.Id == 7) { subjectString = "07"; }
            if (subject.Id == 8) { subjectString = "08"; }
            if (subject.Id == 9) { subjectString = "09"; }
            if (subject.Id == 10) { subjectString = "10"; }
            if (subject.Id == 11) { subjectString = "11"; }
            if (subject.Id == 12) { subjectString = "12"; }
            if (subject.Id == 13) { subjectString = "13"; }
            if (subject.Id == 14) { subjectString = "14"; }
            if (subject.Id == 15) { subjectString = "15"; }
            if (subject.Id == 16) { subjectString = "16"; }
            if (subject.Id == 17) { subjectString = "17"; }
            if (subject.Id == 18) { subjectString = "18"; }
            if (subject.Id == 19) { subjectString = "19"; }
            if (subject.Id == 20) { subjectString = "20"; }
            if (subject.Id == 21) { subjectString = "21"; }
            if (subject.Id == 22) { subjectString = "22"; }
            if (subject.Id != 1 && subject.Id != 2 && subject.Id !=3 && subject.Id != 4 && subject.Id != 5 && subject.Id != 6
                                && subject.Id != 7 && subject.Id != 8 && subject.Id != 9 && subject.Id != 10 && subject.Id != 11 
                                && subject.Id != 12 && subject.Id != 13 && subject.Id != 14 && subject.Id != 15 && subject.Id != 16 
                                && subject.Id != 17 && subject.Id != 18 && subject.Id != 19 && subject.Id != 20 && subject.Id != 21 
                                && subject.Id != 22) { subjectString = "00"; }
            #endregion
            var ranNumber = _uow.PublicationRepository
                                     .GetContentNUmber(_uow.PublicationRepository.GetAll().ToList());
            var generatedContentNumber = $"{licenceString}{categoryString}{subjectString}{ranNumber}";
            try
            {
                var comment = new CurationManagersComment
                {
                    PublicationId = assigment.Id,
                    Notes = model.Notes
                };
                _uow.CurationManagersCommentRepository.Add(comment);
                if (model.ToDo == "Approve")
                {
                    assigment.Approved = true;
                    assigment.CertificateNumber = generatedContentNumber;
                }
                else
                {
                    assigment.Rejected = true;
                }
                _uow.Complete();
                return Ok(value: new { message = "Comments Returned to publisher" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
                publications.Select(p => new PublicationDownloadSerilizerToCurators(p, _uow)).ToList() : new List<PublicationDownloadSerilizerToCurators>();
            return Ok(value: publicationList);
        }
        [HttpGet("get/approved")]
        public IActionResult GetApproved()
        {
            var publications = _uow.PublicationRepository.Find(p => p.Approved).ToList();
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizerToCurators(p, _uow)).ToList() : new List<PublicationDownloadSerilizerToCurators>();
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
                publications.Select(p => new PublicationDownloadSerilizerToCurators(p, _uow)).ToList() : new List<PublicationDownloadSerilizerToCurators>();
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
                publications.Select(p => new PublicationDownloadSerilizerToCurators(p, _uow)).ToList() : new List<PublicationDownloadSerilizerToCurators>();
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
