using Business_Logic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerBL _customerBL;
        private IOrderBL _orderBL;
        private readonly UserManager<Customer> _userManager;
        private ILineItemsBL _lineItemsBL;
        public CustomerController(ICustomerBL p_custBL, IOrderBL p_orderBL, UserManager<Customer> userManager, ILineItemsBL p_lineItemsBL)
        {
            _customerBL = p_custBL;
            _orderBL = p_orderBL;
            _userManager = userManager;
            _lineItemsBL = p_lineItemsBL;
        }

        // GET: HomeController1
        public ActionResult Index(string p_sort)
        {
            List<Order> listOfOrders = _orderBL.GetOrdersListForCustomer(_userManager.GetUserId(User));
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
            foreach (LineItems item in orderToShow.LineItems)
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
