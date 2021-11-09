using Business_Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class StoreFrontController : Controller
    {
        private IStoreFrontBL _storeBL;
        public StoreFrontController(IStoreFrontBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }
        // GET: StoreFront
        public ActionResult Index()
        {
            return View(_storeBL.GetStoreFrontList()
                        .Select(store => new StoreFrontVM(store))
                        .ToList()
                );
        }

        // GET: StoreFront/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoreFront/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreFront/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreFront/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoreFront/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreFront/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoreFront/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
