using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access_Logic.Entities
{
    public partial class Storefront
    {
        public Storefront()
        {
            LineItems = new HashSet<LineItem>();
            Orders = new HashSet<Order>();
        }

        public int StorefrontId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<LineItem> LineItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
