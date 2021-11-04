using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access_Logic.Entities
{
    public partial class LineItem
    {
        public LineItem()
        {
            LineItemOrders = new HashSet<LineItemOrder>();
        }

        public int LineItemId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int? StorefrontId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Storefront Storefront { get; set; }
        public virtual ICollection<LineItemOrder> LineItemOrders { get; set; }
    }
}
