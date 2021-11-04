using System;
using System.Collections.Generic;

namespace Models
{
    public class LineItems
    {
        private Product _product;
        private int _quantity;
        public int LineItemsId { get; set; }
        public int ProductId { get; set; }
        public int StoreFrontId { get; set; }
        public Product Product 
        { 
            get
            {
                return _product;
            }
            set
            {
                _product = value;
            }
        }
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {   
                _quantity = value;                
            }
        }
        public virtual StoreFront StoreFront { get; set; }
        public virtual List<LineItemOrder> LineItemOrders { get; set; }
        public override string ToString()
        {
            return $"Brand: {Product.Brand} \nName: {Product.Name} \nPrice: {Product.Price} \nDescription: {Product.Description} \nCategory: {Product.Category} \nQuantity: {Quantity}";
        }
    }
}