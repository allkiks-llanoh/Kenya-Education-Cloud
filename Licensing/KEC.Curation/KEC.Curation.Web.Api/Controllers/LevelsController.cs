using System;
using System.Collections.Generic;
using System.Linq;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Web.Api.Cors;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{
    [AllowCrossSiteJson]
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
            var levels = _uow.LevelRepository.GetAll().ToList();
            var LevelsList = levels.Any()?
                levels.Select (p=> new LevelsDownloadSerilizer(p, _uow)) : new List<LevelsDownloadSerilizer>();
            return Ok(value: LevelsList.ToList());

        }
       
        
        // POST: api/Levels
        [HttpPost]
       
        public IActionResult CreateLevel([FromBody] LevelsUploadSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            var exist = _uow.LevelRepository.Find(p => p.Name.Equals(model.Name)).Any();
            try
            {
                if (exist) 
                {
                    return BadRequest("Level already exists");
                }
                var level = new Level
                {
                    Name = model.Name,
                    
                };
                _uow.LevelRepository.Add(level);
                _uow.Complete();
                return Ok("Level Created Successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        // Patch: api/Levels/5
        [HttpPatch("{id}")]

        public IActionResult UpdateLevel(int Id, [FromBody]LevelsUploadSerilizer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var level = _uow.LevelRepository.Get(model.Id.GetValueOrDefault());
            if (level == null)
            {
                return NotFound("Level could not be retrieved for updating or is missing ");
            }
            var exist = _uow.LevelRepository.Find(p => p.Name.Equals(model.Name)).Any();
            if (exist)
            {
                return BadRequest("A different Level with the same properties exists");
            }
            else
            {
                level.Name = model.Name;
            }
          
           
            _uow.Complete();
            return Ok("Level Grade updated successfully");
        }
        [HttpDelete("{id}")]

        public IActionResult DeleteLevel(DeleteSerilizer model)
        {

            var level = _uow.LevelRepository.Get(model.Id.GetValueOrDefault());
            if (level == null)
            {
                return NotFound("Level could not be retrieved for deleting or is missing ");
            }
            _uow.LevelRepository.Remove(level);

            _uow.Complete();
            return Ok("Level Deleted From Repository");
        }
    }
}
