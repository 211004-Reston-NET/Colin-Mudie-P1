using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class StoreFrontVM
    {
        public StoreFrontVM(StoreFront p_store)
        {
            this.Name = p_store.Name;
            this.Address = p_store.Address;
            this.StoreFrontId = p_store.StoreFrontId;
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public int StoreFrontId { get; set; }
    }
}
