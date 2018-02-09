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
    [Route("api/CuratorAssignment")]
    public class CuratorAssignmentController : Controller
    {
        private readonly IUnitOfWork _uow;


        public CuratorAssignmentController(IUnitOfWork uow)
        {
            _uow = uow;

        }



        //View List of Chief Curators 
        [HttpGet, Route("ListChiefCurators/{id}")]
        public IActionResult ListChiefCurators(int id)
        {
           
            var curators = _uow.CuratorRepository.Find(p =>
                                p.TypeId.Equals(id));
            if (curators.Any())
            {
                return Ok(value: curators.ToList());
            }
            else
            {
                return NotFound("No Chief Curators Available");
            }
        }

        //Get List Of Curators
        [HttpGet, Route("ListCurators/{id}")]
        public IActionResult ListCurators(int id)
        {

            var curators = _uow.CuratorRepository.Find(p =>
                                p.TypeId.Equals(id));
            if (curators.Any())
            {
                return Ok(value: curators.ToList());
            }
            else
            {
                return NotFound("No Curators Available");
            }
        }

        //Get List For Chief Curator
        [HttpGet, Route("ListForChiefCurator")]
        public IActionResult AllPublishedPublications()
        {
            var stage = (int)PublicationStage.ChiefCurator_New;
            var publications = _uow.PublicationRepository.Find(p => p.PublicationStageLogs
                                .Equals(stage)).ToList();
            if (publications == null)
            {
                return NotFound("No Publications Have Been Assigned By Principal Curator");
            }
            var publicationList = publications.Any() ?
                publications.Select(p => new PublicationDownloadSerilizer(p, _uow)) : new List<PublicationDownloadSerilizer>();
            return Ok(value: publicationList.ToList());
        }
        // POST: api/CuratorAssignment
        [HttpPost, Route("AssignToCurators")]
        public IActionResult AssignToCurator(AssignmentUploadSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            var publicationsectionid = model.PublicationSectionId;
            var notes = model.Notes;
            var assignee = _uow.CuratorRepository.Find(p => p.Id.Equals(model.Id)).FirstOrDefault();
            var assigneename = assignee.GUID;
            var assigndBy= assignee.GUID;


            var assignedby = model.AssignedBy;

            var Assignment = new CuratorAssignment
            {
                PublicationSectionId= publicationsectionid,
                Assignee = assigneename,
                Notes = model.Notes
            };
            var newpublication = new Publication
            {

            };

            newpublication.PublicationStageLogs.Add( new PublicationStageLog
            {
                Stage = PublicationStage.Curation,
                ActionTaken = ActionTaken.PublicationMoveToNextStage
            });

            _uow.CuratorAssignmentRepository.Add(Assignment);
            _uow.Complete();
            return Ok(value:"Assignment to"+" "+assignee.FirstName+" "+assignee.LastName+" "+" Was Succesfull");
            
        }
        // POST: api/CuratorAssignment
        [HttpPost, Route("AssignToChiefCurators")]
        public IActionResult AssignToChiefCurator(AssignmentUploadSerilizer model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            var publicationsectionid = model.PublicationSectionId;
            var notes = model.Notes;
            var assignee = _uow.CuratorRepository.Find(p => p.Id.Equals(model.Id)).FirstOrDefault();
            var assigneename = assignee.GUID;
            var assigndBy = assignee.GUID;


            var assignedby = model.AssignedBy;

            var Assignment = new CuratorAssignment
              {
                Assignee = assigneename,
                Notes = model.Notes
              };
              var newpublication = new Publication
              {

              };

            newpublication.PublicationStageLogs.Add(new PublicationStageLog
             {
                Stage = PublicationStage.ChiefCurator_New,
                ActionTaken = ActionTaken.PublicationMoveToNextStage
             });

             _uow.CuratorAssignmentRepository.Add(Assignment);
             _uow.Complete();
             return Ok(value: "Assignment to" + " " + assignee.FirstName + " " + assignee.LastName + " " + " Was Succesfull");
 
        }

    }
}
