using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access_Logic.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
