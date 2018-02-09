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
    [Route("api/SubjectCategory")]
    public class SubjectCategoryController : Controller
    {
        private readonly IUnitOfWork _uow;

        public SubjectCategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        //Get All Curator Types
        [HttpGet, Route("GetCategories")]
        public IActionResult GetAll()
        {
            var category = _uow.SubjectCategoryRepository.GetAll().ToList();
            var list = category.Any() ?
                category.Select(p => new SubjectCategoryDownloadSerilizer (p)).ToList() : new List<SubjectCategoryDownloadSerilizer>();
            return Ok(value: list);
        }
        // GET: api/SubjectCategory/5
        [HttpGet("{id}", Name = "CategoriesById")]
        public IActionResult CategoriesById(int id)
        {
            var category = _uow.SubjectCategoryRepository.Find(p =>
                            p.Id.Equals(id)).ToList();
            return Ok(value: category.ToList());
        }
        
        // POST: api/SubjectCategory
        [HttpPost]
        public IActionResult CreateCategoryType(string Name, SubjectCategoryUploadSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var exists = _uow.SubjectCategoryRepository.Find(p => p.Name.Equals(model.Name)).Any();
            if (exists)
            {
                return BadRequest(error: "Subject Category already exists");
            }
            var subjectCategory = new SubjectCategory 
            {
                Name = model.Name
            };
            _uow.SubjectCategoryRepository.Add(subjectCategory);
            _uow.Complete();
            return Ok("Subject Category created successfully");
        }
        
        // PUT: api/SubjectCategory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        
    }
}
