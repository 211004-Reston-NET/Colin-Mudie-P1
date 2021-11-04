using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access_Logic.Entities
{
    public partial class LineItemOrder
    {
        public int LineItemId { get; set; }
        public int OrderId { get; set; }
        public int LineItemOrderId { get; set; }

        public virtual LineItem LineItem { get; set; }
        public virtual Order Order { get; set; }
    }
}
