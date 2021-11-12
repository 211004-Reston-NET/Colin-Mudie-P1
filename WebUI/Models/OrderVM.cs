using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class OrderVM
    {
        public OrderVM()
        {
            
        }

        public OrderVM(Order p_order)
        {
            this.OrderId = p_order.OrderId;
            this.Address = p_order.Address;
            this.TotalPrice = p_order.TotalPrice;
            this.StoreFrontId = p_order.StoreFrontId;
            this.CustomerId = p_order.CustomerId;
        }
        
        public int OrderId { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public int StoreFrontId { get; set; }
        public string CustomerId { get; set; }

    }
}