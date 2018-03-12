using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{
    public class AllCurationController : Controller
    {
        // GET: AllCuration
        public ActionResult Index()
        {
            return View();
        }

        // GET: AllCuration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AllCuration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllCuration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AllCuration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AllCuration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AllCuration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AllCuration/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}