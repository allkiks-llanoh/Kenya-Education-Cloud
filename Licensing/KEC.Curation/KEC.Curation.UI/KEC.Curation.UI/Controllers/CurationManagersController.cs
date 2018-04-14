using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    public class CurationManagersController : Controller
    {
        // GET: CurationManagers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Curators()
        {
            return View();
        }

        public ActionResult ChiefCurators()
        {

            return View();
        }
        public ActionResult PrincipalCurators()
        {

            return View();
        }
        public ActionResult AllPublications()
        {

            return View();
        }
        public ActionResult Approved()
        {

            return View();
        }
        public ActionResult PartiallyApproved()
        {

            return View();
        }
        public ActionResult Pending()
        {

            return View();
        }
        public ActionResult Rejected()
        {

            return View();
        }
    }
}
