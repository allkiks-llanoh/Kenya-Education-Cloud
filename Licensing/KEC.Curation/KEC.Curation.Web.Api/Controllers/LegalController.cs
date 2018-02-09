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
    [Route("api/Legal")]
    public class LegalController : Controller
    {
        private readonly IUnitOfWork _uow;

        public LegalController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/Legal
        [HttpGet]
        public IActionResult GetAllPublished()
        {
            var stage = (int)PublicationStage.NewPublication;
            var publications = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals(stage)).ToList();
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)) : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList.ToList());
        }

        // GET: api/Legal/5
        [HttpGet("{id}", Name = "PublicationsByTypeId")]
        public IActionResult PublicationsByTypeId(int id)
        {
             var stage = (int)PublicationStage.NewPublication;
            var publications= _uow.PublicationRepository.Find(p =>
                              p.Id.Equals(id)
                              && p.PublicationStageLogs.Equals(stage)).ToList();

            return Ok(value: publications.ToList());
        }
        
        // POST: api/Legal
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Legal/5
        [HttpPut("approve/{id}")]
        public IActionResult LegalToFinance(FinanceUpdateSerilizer financeUpdateSerilizer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var id = financeUpdateSerilizer.Id;
            var notes = financeUpdateSerilizer.Notes;

            if (notes == null)
            {
                return BadRequest("Cannot Submit Without Comments");

            }

            var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(id)
                              && p.PublicationStageLogs.Equals(PublicationStage.NewPublication)).FirstOrDefault();
            
                if (publication == null)
                {
                    return NotFound("Publication cannot be retrieved or is not yet published");
                }

            var publicationUpdate = new Publication
            {
                
            };

            publicationUpdate.PublicationStageLogs.Add(new PublicationStageLog
            {
                PublicationId=publication.Id,
                Stage = PublicationStage.PaymentVerification,
                Notes= financeUpdateSerilizer.Notes,
                ActionTaken = ActionTaken.PublicationMoveToNextStage

            });

            _uow.PublicationRepository.Add(publicationUpdate);
            _uow.Complete();
            return Ok("Moved to Finance");
        }

        // PUT: api/Legal/5
        [HttpPut("reject/{id}")]
        public IActionResult LegalReject(FinanceUpdateSerilizer financeUpdateSerilizer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var id = financeUpdateSerilizer.Id;
            var notes = financeUpdateSerilizer.Notes;

            if (notes == null)
            {
                return BadRequest("Cannot Submit Without Comments");

            }

            var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(id)
                              && p.PublicationStageLogs.Equals(PublicationStage.NewPublication)).FirstOrDefault();

            if (publication == null)
            {
                return NotFound("Publication cannot be retrieved or is not yet published");
            }

            var publicationUpdate = new Publication
            {

            };

            publicationUpdate.PublicationStageLogs.Add(new PublicationStageLog
            {
                PublicationId = publication.Id,
                Stage = PublicationStage.Rejected,
                Notes = financeUpdateSerilizer.Notes,
                ActionTaken = ActionTaken.PublicationRejected

            });

            _uow.PublicationRepository.Add(publicationUpdate);
            _uow.Complete();
            return Ok("Publication Did Not Meet Legal Requirements");
        }

    }
}
