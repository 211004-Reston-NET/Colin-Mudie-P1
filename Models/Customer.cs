using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Models
{
    public class Customer
    // The customer model is supposed to hold 
    // the data concerning a customer.
    {
        private string _name;

        private string _address;
        private string _email;
        private string _phoneNumber;
        private List<Orders> _orders = new List<Orders>();
        public int CustomerId { get; set; }

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
        public List<Orders> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        public override string ToString()
        {
            return $"Name: {Name} \nAddress: {Address} \nEmail: {Email} \nPhone: {PhoneNumber}";
        }
    }
}