using System;
using System.Collections.Generic;

namespace Models
{
    public class Orders
    // The orders contain information about customer orders.
    {
        private List<LineItems> _lineItems = new List<LineItems>();
        private string _address;
        private decimal _totalPrice;
        public int OrderId { get; set; }
        public int StoreFrontId { get; set; }
        public int CustomerId { get; set; }

        public List<LineItems> LineItems
        {
            get
            {
                return _lineItems;
            }
            set
            {
                _lineItems = value;
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }
        public decimal TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
            }
        }
        public override string ToString()
        {
            string items = "";
            foreach (LineItems item in _lineItems)
            {
                items += 
            $"\n-------------------------"+
            $"\n{item.Product.Name}"+
            $" \nPrice: {item.Product.Price}";
            }
            return $"{items}\n_________________________ \nAddress: {Address}  \nTotal Price: {TotalPrice}";
            
        }
    }
}