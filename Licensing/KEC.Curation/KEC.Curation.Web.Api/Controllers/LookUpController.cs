using KEC.Curation.Data.Models;
using KEC.Curation.Services.Extensions;
using KEC.Curation.Web.Api.Cors;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KEC.Curation.Web.Api.Controllers
{
    [AllowCrossSiteJson]
    [Produces("application/json")]
    [Route("api/lookups")]
    public class LookUpController : Controller
    {
        [HttpGet("actions")]
        public IActionResult Actions()
        {
            var actions = Enum.GetNames(typeof(ActionTaken)).ToList();
            var actionsList = new List<LookUpSerializer>();
            actions.ForEach(p =>
            {
                var actionEnum = (ActionTaken)Enum.Parse(typeof(ActionTaken), p);
                var action = new LookUpSerializer
                {
                    Id = (int)actionEnum,
                    Name = p,
                    Description =actionEnum.GetDescription()
                };
                actionsList.Add(action);
            });
            return Ok(value: actionsList);
        }
    }
}