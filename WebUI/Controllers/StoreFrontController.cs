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
    public class StoreFrontController : Controller
    {
        private IStoreFrontBL _storeBL;
        private IOrderBL _orderBL;
        private ILineItemsBL _lineItemsBL;
        public StoreFrontController(IStoreFrontBL p_storeBL, IOrderBL p_orderBL, ILineItemsBL p_lineItemsBL)
        {
            _storeBL = p_storeBL;
            _orderBL = p_orderBL;
            _lineItemsBL = p_lineItemsBL;
        }
        // GET: StoreFront
        public ActionResult Index()
        {
            return View(_storeBL.GetStoreFrontList()
                        .Select(store => new StoreFrontVM(store))
                        .ToList()
                );
        }

       //public ActionResult OrderList(int p_id)
       // {
       //     StoreFront _storeFound = _storeBL.GetStoreFrontById(p_id);
       //     List<Order> listOfOrders = _orderBL.GetOrdersListForStore(p_id);
       //     return View(listOfOrders
       //                     .Select(ord => new OrderVM(ord))
       //                     .ToList()
       //            );
       // }

        public ActionResult OrderList(int p_id, string p_sort)
        {
            StoreFront _storeFound = _storeBL.GetStoreFrontById(p_id);
            List<Order> listOfOrders = _orderBL.GetOrdersListForStore(p_id);
            switch (p_sort)
            {
                case "OrderAsc":
                    listOfOrders = listOfOrders.OrderBy(ord => ord.OrderId).ToList();
                    break;
                case "OrderDesc":
                    listOfOrders = listOfOrders.OrderByDescending(ord => ord.OrderId).ToList();
                    break;
                case "PriceAsc":
                    listOfOrders = listOfOrders.OrderBy(ord => ord.TotalPrice).ToList();
                    break;
                case "PriceDesc":
                    listOfOrders = listOfOrders.OrderByDescending(ord => ord.TotalPrice).ToList();
                    break;
                default:
                    break;
            }
            return View(listOfOrders
                            .Select(ord => new OrderVM(ord))
                            .ToList()
                    );
        }

        public ActionResult PreviousOrder(int p_orderId)
        {
            List<LineItems> itemList = new List<LineItems>();
            Order orderToShow = _orderBL.GetOrderById(p_orderId);
            foreach(LineItems item in orderToShow.LineItems)
            {
                itemList.Add(_lineItemsBL.GetLineItemsById(item.LineItemsId));
            }

            ViewData.Add("OrderId", orderToShow.OrderId);
            ViewData.Add("Address", orderToShow.Address);
            ViewData.Add("CustomerName", orderToShow.Customer.Name);
            ViewData.Add("TotalPrice", orderToShow.TotalPrice);
            
            return View(itemList
                            .Select(item => new LineItemVM(item))
                            .ToList()
                       );
        }
    }
}
