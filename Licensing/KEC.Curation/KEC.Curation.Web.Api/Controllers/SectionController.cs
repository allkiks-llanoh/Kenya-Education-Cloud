using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace KEC.Curation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Section")]
    public class SectionController : Controller
    {
        private readonly IUnitOfWork _uow;
        public SectionController(IUnitOfWork uow)
        {
            _uow = uow;

        }
        // GET: api/Section/5
        //Get Section by publicationID
        [HttpGet("{id}", Name = "PublicationId")]
        public IActionResult SectionById(int id)
        {
         
            var section = _uow.PublicationSectionRepository.Find(p =>
                               p.PublicationId.Equals(id)).ToList();

            return Ok(value: section.ToList());
        }
        // POST: api/Section
        [HttpPost("{id}")]
        public IActionResult CreateSection(int id, SectionsUploadSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository.Find(p => 
                              p.Id.Equals(id)).FirstOrDefault();
            var section = new PublicationSection
            {
                PublicationId=publication.Id,
                SectionDescription=model.SectionDescription,
                Owner=publication.Owner,
                CreatedAtUtc = DateTime.UtcNow
            };
            _uow.PublicationSectionRepository.Add(section);
            _uow.Complete();
            return Ok(value:"Section added for "+publication.Title+" Succesfully");
        }
        
        // PUT: api/Section/5
        [HttpPut("{id}")]
        public IActionResult UpdateSection(int id, SectionsUploadSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var publication = _uow.PublicationRepository.Find(p =>
                              p.Id.Equals(id)).FirstOrDefault();
            var section = new PublicationSection
            {
                PublicationId = publication.Id,
                SectionDescription = model.SectionDescription,
                Owner = publication.Owner,
                CreatedAtUtc = DateTime.UtcNow
            };
            _uow.PublicationSectionRepository.Add(section);
            _uow.Complete();
            return Ok(value: "Section Updated for " + publication.Title + " Succesfully");
        }
    }
}
