using System;
using System.Collections.Generic;

namespace Models
{
    public class StoreFront
    // The store front contains information pertaining the various store locations
    {
        private string _name;
        private string _address;
        private List<LineItems> _lineItems;
        private List<Order> _order;
        public int StoreFrontId { get; set; }

        //properties
        public string Name { 
            get
            {
                return _name;
            }
            set
            { 
                _name = value;
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
        public List<Order> Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }

        public override string ToString()
        {
            return $"Name: {Name} \nAddress: {Address}";
        }
    }
}