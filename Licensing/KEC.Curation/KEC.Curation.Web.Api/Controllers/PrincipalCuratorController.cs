using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{

    [EnableCors("*")]
    [Produces("application/json")]
    [Route("api/PrincipalCurator")]
    public class PrincipalCuratorController : Controller
    {

        private readonly IUnitOfWork _uow;
        

        public PrincipalCuratorController(IUnitOfWork uow)
        {
            _uow = uow;
          
        }

        [HttpGet]
        public IActionResult Principal()
        {

            var publicatons = _uow.PublicationRepository.Find(p => p.PublicationStageLogs.Equals
                              (PublicationStage.PrincipalCurator));
            var publicationList = publicatons.Any() ?
                publicatons.Select(p => new PublicationDownloadSerilizer(p, _uow)).ToList() : new List<PublicationDownloadSerilizer>();
            return Ok(publicationList);


        }

       
        
        // POST: api/publicationId/assign
        [HttpPost("{id}/assign")]
        public IActionResult Assign(int publicationId, [FromBody]ChiefCuratorAssignmentSerializer model)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository
                                  .Find(p => p.Id.Equals(publicationId))
                                  .FirstOrDefault();

            if (publication == null)
            {
                return NotFound("Publication could not be retrieved for assignment.");
            }
            var maxStage = _uow.PublicationStageLogRepository.Find(p => p.PublicationId == publication.Id).Max(p => p.Stage);
            var publicationLog = _uow.PublicationStageLogRepository.Find(p => p.Stage.Equals(PublicationStage.PrincipalCurator)
                                                            && p.Stage == maxStage
                                                            && p.PublicationId.Equals(publication.Id)).FirstOrDefault();

            try
            {

                var assignment = new ChiefCuratorAssignment
                {
                    PublicationId = publication.Id,
                    PrincipalCuratorGuid = "TODO",
                    ChiefCuratorGuid = "TODO",
                    AssignmetDateUtc = DateTime.UtcNow


                };
                 publicationLog.ActionTaken = ActionTaken.PublicationMoveToNextStage;
                _uow.ChiefCuratorAssignmentRepository.Add(assignment);
                _uow.PublicationRepository.ProcessToTheNextStage(publication);
                _uow.Complete();
                return Ok(value: "Content assigned to chief curator");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        } 
        
        // PUT: api/PrincipalCurator/5
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
