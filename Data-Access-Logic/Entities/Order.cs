using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access_Logic.Entities
{
    public partial class Order
    {
        public Order()
        {
            LineItemOrders = new HashSet<LineItemOrder>();
        }

        public int OrderId { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public int StorefrontId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Storefront Storefront { get; set; }
        public virtual ICollection<LineItemOrder> LineItemOrders { get; set; }
    }
}
