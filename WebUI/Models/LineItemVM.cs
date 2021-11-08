using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class LineItemVM
    {
        public LineItemVM()
        {
            
        }

        public LineItemVM(LineItems p_item)
        {
            this.LineItemId = p_item.LineItemsId;
            this.Quantity = p_item.Quantity;
            this.ProductId = p_item.ProductId;
            this.StoreFrontId = p_item.StoreFrontId;
        }
        public int LineItemId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int StoreFrontId { get; set; }
    }
}