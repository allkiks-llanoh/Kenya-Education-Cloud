using KEC.Curation.UI.ActionFilters;
using KEC.Curation.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    [CustomAuthorize(Roles = "Curator")]
    [AllowCrossSiteJson]
    [UserGuidJson]
   
    public class CuratorController : Controller
    {
        public ActionResult Curator()
        {
            ViewData["SubTitle"] = "Assigned Publications";
            ViewData["Message"] = " ";
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));

                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName = user.FullName
                };

                return View(chiefCurator);

            }

        }
        public ActionResult List()
        {
            ViewData["SubTitle"] = "Assigned Publications";
            ViewData["Message"] = " ";
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));

                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName = user.FullName
                };

                return View(chiefCurator);

            }

        }
        public ActionResult ToCurate(string curatorGuid)
        {
           
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName= user.FullName
                };
                return View(chiefCurator);
            }
        }
       
        public ActionResult CuratorView(string userguidGuid)
        {

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName = user.FullName
                };
                return View(chiefCurator);
            }
        }
  
        public ActionResult CuratePublication(int Id)
        {
            ViewBag.AssignmentId = Id;
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName = user.FullName
                };
                return View(chiefCurator);
            }
        }
        //public ActionResult GetCuration()
        //{
        //    IEnumerable<ToCurate> toCutate = null;
        //    using (var client = new HttpClient())
        //    {
        //        //var uri = new Uri("https://curationapi-d.kec.ac.ke/api/chiefcurator/curator/tocurate?userGuid=1cb673c8-a921-4a9f-b42f-69050a70aae6");
        //        client.BaseAddress = new Uri("https://curationapi-d.kec.ac.ke/api/chiefcurator/curator/");
        //        //HTTP GET
        //        var responseTask = client.GetAsync("tocurate?userGuid=1cb673c8-a921-4a9f-b42f-69050a70aae6");

        //        responseTask.Wait();

        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<IList<ToCurate>>();
        //            readTask.Wait();

        //            toCutate = readTask.Result;
        //        }
        //        else //web api sent error response 
        //        {
        //            //log response status here..

        //            toCutate = Enumerable.Empty<ToCurate>();

        //            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
        //        }
        //    }
        //    return View(toCutate);
        //}
        
    }
}