using Business_Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class LineItemController : Controller
    {
        private ILineItemsBL _lineItemBL;
        public LineItemController(ILineItemsBL p_lineItem)
        {
            _lineItemBL = p_lineItem;
        }
        // GET: LineItem
        public ActionResult Index(int p_storeId)
        {
            return View(_lineItemBL.GetLineItems(p_storeId)
                        .Select(item => new LineItemVM(item))
                        .ToList() 
                );
        }

        // GET: LineItem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LineItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LineItem/Create
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

        // GET: LineItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LineItem/Edit/5
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

        // GET: LineItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LineItem/Delete/5
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
