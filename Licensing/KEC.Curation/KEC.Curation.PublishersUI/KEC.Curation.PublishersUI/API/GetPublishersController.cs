using KEC.Curation.PublishersUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace KEC.Curation.PublishersUI.API
{
    public class GetPublishersController : ApiController
    {
        private readonly ApplicationDbContext  context = new ApplicationDbContext();
        // GET: api/GetPublishers
        [HttpGet]
        public IHttpActionResult Publishers()
        {
            context.Configuration.LazyLoadingEnabled = false;
            var users = context.Users.OrderBy(x => x.Id).ToList();
            return Ok(users);
        }
       
    }
}
