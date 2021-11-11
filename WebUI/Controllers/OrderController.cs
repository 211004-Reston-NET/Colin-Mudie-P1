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
    public class OrderController : Controller
    {
        private IOrderBL _orderBL;
        private ILineItemsBL _lineItemsBL;
        private IStoreFrontBL _storeFrontBL;
        private static List<LineItems> _currentOrder = new List<LineItems>();
        public OrderController(IOrderBL p_orderBL, ILineItemsBL p_lineItemsBL, IStoreFrontBL p_storeFrontBL)
        {
            _orderBL = p_orderBL;
            _lineItemsBL = p_lineItemsBL;
            _storeFrontBL = p_storeFrontBL;
        }

        // GET: OrderController

        public ActionResult Index(int p_StoreId, int p_lineItemId)
        {
            LineItems itemToAdd = _lineItemsBL.GetLineItemsById(p_lineItemId);
            if (itemToAdd != null)
            {
                itemToAdd.Quantity = 1;
                _currentOrder.Add(itemToAdd);
                decimal totalPrice = _currentOrder.Sum(item => item.Product.Price);
                if (totalPrice >= 0)
                {
                    ViewData.Add("TotalPrice", totalPrice);
                }
                else
                {
                    ViewData.Add("TotalPrice", 0);
                }
            }          
            return View(_currentOrder
                            .Select(item => new LineItemVM(item, _storeFrontBL.GetStoreFrontById(item.StoreFrontId)))
                            .ToList());
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int p_lineItemId)
        {
            return View(new LineItemVM(_lineItemsBL.GetLineItemsById(p_lineItemId)));
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int LineItemId, IFormCollection collection)
        {
            try
            {
                LineItems toBeDeleted = _currentOrder.FirstOrDefault(item => item.LineItemsId == LineItemId);
                _currentOrder.Remove(toBeDeleted);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
