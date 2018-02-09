using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KEC.Curation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Finance")]
    public class FinanceController : Controller
    {
        private readonly IUnitOfWork _uow;

        public FinanceController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/Finance
        [HttpGet]
        public IActionResult AllPublishedPublications()
        {
            var stage = (int)PublicationStage.PaymentVerification;
            var publications = _uow.PublicationRepository.Find(p => p.PublicationStageLogs
                                .Equals(stage)).ToList();
                if (publications == null)
                 {
                return NotFound("No Publications Have Been Submitted To Finance Yet");
                 }
            var publicationList = publications.Any()?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)) : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList.ToList());
        }
        // PUT: api/Finance/5
        [HttpPut("{id}")]
        public IActionResult ChangeStage([FromBody] FinanceUpdateSerilizer financeUpdateSerilizer)
        {
            var id = financeUpdateSerilizer.Id;
            var notes = financeUpdateSerilizer.Notes;

            if (notes == null)
            {
                return BadRequest("Cannot Submit Without Comments");
            }
                  
           

            var publicationUpdate = new Publication
            {
                
            };


            publicationUpdate.PublicationStageLogs.Add(new PublicationStageLog
            {
                Stage = PublicationStage.PrincipalCuratorLevel,
                Notes = financeUpdateSerilizer.Notes,
                ActionTaken = ActionTaken.PublicationMoveToNextStage

            });
            _uow.PublicationRepository.Add(publicationUpdate);
          
            _uow.Complete();
            return Ok("Payment has been validated, Moved To Principal Curator");
        }
        
       
    }
}
