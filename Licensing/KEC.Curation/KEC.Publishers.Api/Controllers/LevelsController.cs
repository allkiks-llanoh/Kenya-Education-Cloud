using System.Collections.Generic;
using System.Linq;
using KEC.Curation.Data.UnitOfWork;
using KEC.Publishers.Api.Cors;
using KEC.Publishers.Api.Serializers;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Publishers.Api.Controllers
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
            var LevelsList = levels.Any() ?
                levels.Select(p => new LevelsDownloadSerilizer(p, _uow)) : new List<LevelsDownloadSerilizer>();
            return Ok(value: LevelsList.ToList());
        }
         
    }
}