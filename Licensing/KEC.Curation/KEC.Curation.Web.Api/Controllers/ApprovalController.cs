using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KEC.Curation.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KEC.Curation.Data;
using KEC.Curation.Data.Models;
using KEC.Curation.Web.Api.Serializers;

namespace KEC.Curation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Approval")]
    public class ApprovalController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ApprovalController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        //Perform Get For Chief Curator
        //API/Approval/ChiefCurator
        [HttpGet,Route("ChiefCurator")]
        public IActionResult ChiefCuratorPublications()
        {

            var publications = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.
                                Equals(PublicationStage.Curated)).ToList();
            if (publications == null)
            {
                return NotFound("No Publications Have Gone Though The Curation Stage Yet");
            }
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)) : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList.ToList());
        }
        //Perform Get For Principal Curator
        //API/Approval/PrincipalCurator
        [HttpGet, Route("PrincipalCurator")]
        public IActionResult PrincipalCuratorPublications()
        {

            var publications = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.
                                Equals(PublicationStage.ChiefCurator_Approved)).ToList();
            if (publications == null)
            {
                return NotFound("No Publications Have Been Approved by Chif Curator");
            }
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)) : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList.ToList());
        }
        // POST: api/Approval/chiefapprove
        [HttpPost, Route("ChiefApprove")]
        public IActionResult ChiefCuratorApprove(ApprovalSerilizer model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);

            }
            var id = model.Id;
            var publication = _uow.PublicationRepository.Find(p =>
                               p.Id.Equals(id)).FirstOrDefault();

            var newpublication = new Publication
            {

            };
            publication.PublicationStageLogs.Add(new PublicationStageLog
            {
                Stage = PublicationStage.ChiefCurator_Approved,
                Notes = model.Notes,
                ActionTaken = ActionTaken.PublicationMoveToNextStage
            });
            _uow.PublicationRepository.Add(newpublication);
            _uow.Complete();
            return Ok(value: "Sent To Principal Curator");

        }
        // POST: api/Approval/chiefapprove
        [HttpPost, Route("PrincipalApprove")]
        public IActionResult PrincipalCuratorApprove(ApprovalSerilizer model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);

            }
            var id = model.Id;
            var publication = _uow.PublicationRepository.Find(p =>
                               p.Id.Equals(id)).FirstOrDefault();

            var newpublication = new Publication
            {

            };
            publication.PublicationStageLogs.Add(new PublicationStageLog
            {
                Stage = PublicationStage.Approved,
                Notes = model.Notes,
                ActionTaken = ActionTaken.PublicationApproved
            });
            _uow.PublicationRepository.Add(newpublication);
            _uow.Complete();
            return Ok(value: "Publication Approved : Certificate Can Now Be Generated");

        }

        // PUT: api/Approval/5
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
