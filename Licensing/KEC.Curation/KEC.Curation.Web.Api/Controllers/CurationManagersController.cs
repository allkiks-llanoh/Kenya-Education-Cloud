using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var publicationsCount = _uow.PublicationRepository.Find(p => p.Rejected && p.Owner.Equals(guid)).ToList();

            return Ok(value: publicationsCount);
        }
       
        [HttpGet("get/pending/publisher/{guid}")]
        public IActionResult GetPending(string guid)
        {

            var publicationsCount = _uow.PublicationRepository.Find(p => !p.Rejected && p.Owner.Equals(guid) && !p.Approved && p.PublicationStageLogs.Max(l => l.Stage)
                                       == PublicationStage.PublicationApproval).ToList();

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
            var assigment = _uow.PublicationRepository.Find(p => p.Id.Equals(model.PublicationId)
                                                                     && p.PublicationStageLogs.Max(l => l.Stage)
                                                                     == PublicationStage.PublicationApproval)
                                                                    .FirstOrDefault();
            if (assigment == null)
            {
                return NotFound("Record could not be retrieved");
            }
            try
            {
                var comment = new CurationManagersComment
                {
                    PublicationId= assigment.Id,
                    Notes = model.Notes
                };
                _uow.CurationManagersCommentRepository.Add(comment);
                if (model.ToDo == "Approve")
                {
                    assigment.Approved = true;
                    assigment.CertificateNumber = _uow.PublicationRepository
                                      .GetContentNUmber(_uow.PublicationRepository.GetAll().ToList());
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

            var publicationsCount = _uow.PublicationRepository.Find(p=> p.Id.Equals(Id)).FirstOrDefault();

            return Ok(value: publicationsCount);
        }
        [HttpGet("get/publications")]
        public IActionResult GetAllPublications()
        {

            var publicationsCount = _uow.PublicationRepository.GetAll().ToList();

            return Ok(value: publicationsCount);
        }
        [HttpGet("get/approved")]
        public IActionResult GetApproved()
        {

            var publicationsCount = _uow.PublicationRepository.Find(p => p.Approved).ToList();

            return Ok(value: publicationsCount);
        }
        [HttpGet("get/approved/{Id}")]
        public IActionResult GetApprovedById(int Id)
        {

            var publicationsCount = _uow.PublicationRepository.Find(p => p.Approved &&p.Id.Equals(Id)).FirstOrDefault();

            return Ok(value: publicationsCount);
        }
        [HttpGet("get/rejected")]
        public IActionResult GetRejected()
        {

            var publicationsCount = _uow.PublicationRepository.Find(p => p.Rejected).ToList();

            return Ok(value: publicationsCount);
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

            var publicationsCount = _uow.PublicationRepository.Find(p => !p.Rejected && !p.Approved && p.PublicationStageLogs.Max(l => l.Stage)
                                       == PublicationStage.PublicationApproval).ToList();

            return Ok(value: publicationsCount);
        }
       //Pending Publications By Id
        [HttpGet("get/pending/{id}")]
        public IActionResult GetPendingById(int Id)
        {

            var publicationsCount = _uow.PublicationRepository.Find(p => !p.Rejected && !p.Approved && p.PublicationStageLogs.Max(l => l.Stage)
                                       == PublicationStage.PublicationApproval
                                       &&p.Id.Equals(Id)).FirstOrDefault();

            return Ok(value: publicationsCount);
        }
        #endregion
    }
}
