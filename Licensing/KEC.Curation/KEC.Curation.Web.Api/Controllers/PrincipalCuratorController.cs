using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace KEC.Curation.Web.Api.Controllers
{

    [AllowCrossSiteJson]
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
            return Ok(value:publicationList);


        }
        [HttpGet("Assigned")]
        public IActionResult Assigned()
        {

            var publicatons = _uow.ChiefCuratorAssignmentRepository.GetAll().ToList();
          
            return Ok(value: publicatons);


        }
        [HttpGet("{stage}")]
        public IActionResult PublicationsByStage(PublicationStage stage)
        {
            try
            {

                var stageLevel = (int)stage;
                var publicationIds = _uow.PublicationRepository
                                         .Find(p => p.PublicationStageLogs.Count == stageLevel
                                               && !p.PublicationStageLogs.Any(l => l.Stage > stage)

                                               && !p.PublicationStageLogs

                                                   .Any(l => l.ActionTaken == ActionTaken.PublicationRejected))
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
        // POST: api/PrincipalCurator/publication/publicationId/assign
        [HttpPost("publication/{publicationId:int}/assign")]
        public IActionResult Assign(int publicationId,[FromBody]ChiefCuratorAssignmentSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository
                                  .Find(p => p.Id.Equals(publicationId)).FirstOrDefault();
            if (publication == null)
            {
                return NotFound(value: new { message = "Publication could not be retrieved for assignment." });
            }
           
            try
            {
                var assignment = new ChiefCuratorAssignment
                {
                    PublicationId = publication.Id,
                    PrincipalCuratorGuid = model.PrincipalCuratorGuid,
                    ChiefCuratorGuid = model.ChiefCuratorGuid,
                    AssignmetDateUtc = DateTime.UtcNow

                };    
                publication.PublicationStageLogs.Add(new PublicationStageLog
                {
                    Stage = PublicationStage.Curation,
                    Owner = publication.Owner,
                    CreatedAtUtc = DateTime.UtcNow,
                    Notes = model.Notes,
                    

                });
                _uow.ChiefCuratorAssignmentRepository.Add(assignment);
              
                _uow.Complete();
                return Ok(value: new { message = "Content assigned successfully" });
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
