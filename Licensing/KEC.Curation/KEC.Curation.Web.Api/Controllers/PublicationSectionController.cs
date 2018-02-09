using System.Collections.Generic;
using System.Linq;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/PublicationSection")]
    public class PublicationSectionController : Controller
    {

        private readonly IUnitOfWork _uow;

        public PublicationSectionController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/PublicationSection
        [HttpGet]
        public IActionResult GetAllSections()
        {
            //var publicationSections = _uow.PublicationSectionRepository.GetAll().ToList();
            //var publicationSectionList = publicationSections.Any() ?
            //    publicationSections.Select(p => new SectionsDownloadSerilizer(p)).ToList() : new List<SectionsDownloadSerilizer>();

            //return Ok(value: publicationSectionList);
            return Ok();
        }

     
        
        // POST: api/PublicationSection
        [HttpPost]
        public IActionResult Post([FromBody]SectionsUploadSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState:ModelState);
            }
           
            try
            {
                var SectionDescription = model.SectionDescription;
                if (SectionDescription == null)
                {
                    return BadRequest("SectionDescription is required");

                }
                var PublicationSection = new PublicationSection
                {
                    SectionDescription = model.SectionDescription,

                };
                _uow.PublicationSectionRepository.Add(PublicationSection);
                _uow.Complete();
                return Ok("Section Added Succesfully");
            }
            catch
            {
                return BadRequest();
            }
        }
        
        // PUT: api/PublicationSection/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
       
    }
}
