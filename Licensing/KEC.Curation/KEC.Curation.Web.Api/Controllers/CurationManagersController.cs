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
        [HttpGet("get/rejected")]
        public IActionResult GetRejected()
        {

            var publicationsCount = _uow.PublicationRepository.Find(p => p.Rejected).ToList();

            return Ok(value: publicationsCount);
        }
        [HttpGet("get/pending")]
        public IActionResult GetPending()
        {

            var publicationsCount = _uow.PublicationRepository.Find(p => !p.Rejected && !p.Approved && p.PublicationStageLogs.Max(l => l.Stage)
                                       == PublicationStage.PublicationApproval).ToList();

            return Ok(value: publicationsCount);
        }
        #endregion
    }
}
