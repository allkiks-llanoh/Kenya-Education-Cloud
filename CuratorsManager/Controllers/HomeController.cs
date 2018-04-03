using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using curatorsManager.Models;

namespace curatorsManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Curators()
        {
                      return View();
        }

        public IActionResult ChiefCurators()
        {
           
            return View();
        }
         public IActionResult PrincipalCurators()
        {
           
            return View();
        }
         public IActionResult AllPublications()
        {
           
            return View();
        }
         public IActionResult Approved()
        {
           
            return View();
        }
         public IActionResult PartiallyApproved()
        {
           
            return View();
        }
         public IActionResult Rejected()
        {
           
            return View();
        }




        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
