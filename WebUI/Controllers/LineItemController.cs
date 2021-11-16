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

        // GET: LineItem/Edit/5
        public ActionResult Edit(int p_id)
        {
            LineItems itemFound = _lineItemBL.GetLineItemsById(p_id);
            return View(new LineItemVM(itemFound));
        }

        // POST: LineItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LineItemVM p_lineItemVM, int LineItemId)
        {
            try
            {
                LineItems itemToUpdate = _lineItemBL.GetLineItemsById(LineItemId);
                itemToUpdate.Quantity = p_lineItemVM.Quantity;
                _lineItemBL.RefreshStock(itemToUpdate);
                return RedirectToAction("Index", new
                {
                    p_storeId = itemToUpdate.StoreFrontId
                });
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception.Message);
                return View();
            }
        }
    }
}
