using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
// using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class Customer : IdentityUser
    // The customer model is supposed to hold 
    // the data concerning a customer.
    {
        private string _name;

        private string _address;
        private string _email;
        private string _phoneNumber;
        private List<Order> _order = new List<Order>();
        public int CustomerId { get; set; }

        [PersonalData]
        public string Name
        {
            get { return _name; }
            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+(\s[a-zA-Z]+)?$"))
                {
                    throw new Exception("Name can only hold letters.");
                }
                _name = value;
            }
        }

        [PersonalData]
        public string Address
        {
            get { return _address; }
            set
            {
                if (!Regex.IsMatch(value, @"^[A-Za-z0-9'\.\-\s\,]"))
                {
                    throw new Exception("Address must follow standard address format.");
                }
                _address = value;
            }
        }

        [PersonalData]
        public string Email
        {
            get { return _email; }
            set
            {
                if (!Regex.IsMatch(value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    throw new Exception("Not a valid email address");
                }
                _email = value;
            }
        }

        [PersonalData]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (!Regex.IsMatch(value, @"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$"))
                {
                    throw new Exception("Not a valid phone number");
                }
                _phoneNumber = value;
            }
        }
        public List<Order> Order
        {
            get { return _order; }
            set { _order = value; }
        }
    }
}