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
    [Route("api/PrincipalCurator")]
    public class PrincipalCuratorController : Controller
    {
        private readonly IUnitOfWork _uow;

        public PrincipalCuratorController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/PrincipalCurator
        [HttpGet]
        public IActionResult PaidPublications()
        {
            var publications = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals(PublicationStage.PrincipalCuratorLevel)).ToList();
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer (p, _uow)) : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList.ToList());
        }

       
        
        // POST: api/PrincipalCurator
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/PrincipalCurator/5
        [HttpPut("{id}")]
        public IActionResult FinanceToPrincipalCurator(FinanceUpdateSerilizer financeUpdateSerilizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState:ModelState);
            }
            var id = financeUpdateSerilizer.Id;
            var notes = financeUpdateSerilizer.Notes;

            if (notes == null)
            {
                return BadRequest("Cannot Submit Without Comments");

            }

            var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(id)
                              && p.PublicationStageLogs.Equals(PublicationStage.PrincipalCuratorLevel)).FirstOrDefault();

            if (publication == null)
            {
                return NotFound("Publication cannot be retrieved or is not yet approved in Finance");
            }


            var publicationUpdate = new Publication
            {
                
            };

            publicationUpdate.PublicationStageLogs.Add(new PublicationStageLog
            {
                PublicationId = publication.Id,
                Stage = PublicationStage.Curation,
                Notes = financeUpdateSerilizer.Notes,
                ActionTaken = ActionTaken.PublicationMoveToNextStage

            });

            _uow.PublicationRepository.Add(publicationUpdate);
           
            _uow.Complete();
            return Ok("Moved to Chief Curator");
        }
        
        
    }
}
