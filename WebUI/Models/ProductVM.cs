using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class ProductVM
    {
        public ProductVM()
        {
            
        }

        public ProductVM(Product p_prod)
        {
            this.ProductId = p_prod.ProductId;
            this.Name = p_prod.Name;
            this.Price = p_prod.Price;
            this.Brand = p_prod.Brand;
            this.Category = p_prod.Category;
            this.Description = p_prod.Description;
        }
        
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }

        public string Description { get; set; }
    }
}