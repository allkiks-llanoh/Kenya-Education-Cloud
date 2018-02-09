using System;
using System.Collections.Generic;
using System.Linq;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Levels")]
    public class LevelsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public LevelsController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/Levels
        [HttpGet]
        public IActionResult AllLevels()
        {
            var levels = _uow.levelRepository.GetAll().ToList();
            var LevelsList = levels.Any()?
                levels.Select (p=> new LevelsDownloadSerilizer(p, _uow)) : new List<LevelsDownloadSerilizer>();
            return Ok(value: LevelsList.ToList());

        }
       
        
        // POST: api/Levels
        [HttpPost]
        public IActionResult CreateLevel(string Name, LevelsUploadSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var exist = _uow.levelRepository.Find(p => p.Name.Equals(model.Name)).Any();
            try
            {
                if (exist)
                {
                    return BadRequest("Level Grade already exists");
                }
                var level = new Level
                {
                    Name = model.Name,
                    
                };
                _uow.levelRepository.Add(level);
                _uow.Complete();
                return Ok("Level Grade Created Successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        // PUT: api/Levels/5
        [HttpPut("{id}")]
        public IActionResult UpdateLevel(int Id, LevelsUploadSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var level = _uow.levelRepository.Get(model.Id.GetValueOrDefault());
            if (level == null)
            {
                return NotFound("Level Grade could not retrieved for updating or is missing ");
            }
            var exist = _uow.levelRepository.Find(p => p.Name.Equals(model.Name)).Any();
            if (exist)
            {
                return BadRequest("A different Level Grade with the same properties exists");
            }
            level.Name = model.Name;
           
            _uow.Complete();
            return Ok("Level Grade updated successfully");
        }
        
       
    }
}
