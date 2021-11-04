using System;
using Models;
using Xunit;

namespace MMTest
{
    public class CustomerModelTestCases
    {

            // ----------- NAME ----------

        /// <summary>
        /// Will test if the Name property will set with valid data.
        /// valid data - anything with letters only.
        /// </summary>
        [Fact]
        public void NameShouldSetValidData()
        {
            //Arrange
            Customer _customerTest = new Customer();
            string _name = "TestName";
            //Act
            _customerTest.Name = _name;
            //Assert
            Assert.NotNull(_customerTest.Name);
            Assert.Equal(_customerTest.Name, _name);
        }

        /// <summary>
        /// Will test if Name property gives an exception if given invalid data.
        /// invalid data - any numbers or special characters OR empty string.
        /// </summary>
        [Theory]
        [InlineData("K*le")]
        [InlineData("12345")]
        [InlineData("")]
        public void NameShouldFailIfSetWithInvalidData(string p_input)
        {
            Customer _customerTest = new Customer();
            Assert.Throws<Exception>(() => _customerTest.Name = p_input);
        }


            // ---------- Address ----------

        /// <summary>
        /// Will test if Address property will set with valid data.
        /// valid data - anything with letters and numbers.
        /// </summary>
        [Fact]
        public void AddressShouldSetValidData()
        {
            Customer _customerTest = new Customer();
            string _address = "123 Main St.";

            _customerTest.Address = _address;

            Assert.NotNull(_customerTest.Address);
            Assert.Equal(_customerTest.Address, _address);
        }

        /// <summary>
        /// Will test if Address property gives an exception if given invalid data.
        /// invalid data - special characters outside of comma, period, and single-quote OR empty string.
        /// </summary>
        [Theory]
        [InlineData("")]
        public void AddressShouldFailIfSetWithInvalidData(string p_input)
        {
            Customer _customerTest = new Customer();
            Assert.Throws<Exception>(() => _customerTest.Address = p_input);
        }

            // ---------- Email ----------

        /// <summary>
        /// Will test if Email property gives an exception if given valid data.
        /// valid data - standard email format (letters or numbers, then "@", then more letters or numbers, then ".", then more letters.)
        /// </summary>
        [Fact]
        public void EmailShouldSetValidData()
        {
            Customer _customerTest = new Customer();
            string _email = "test@example.com";
            _customerTest.Email = _email;
            Assert.NotNull(_customerTest.Email);
            Assert.Equal(_customerTest.Email, _email);
        }

        /// <summary>
        /// Will test if Email property gives an exception if given invalid data.
        /// invalid data - not comforming to standard email format (letters or numbers, then "@", then more letters or numbers, then ".", then more letters.)
        ///     OR empty string.
        /// </summary>
        [Theory]
        [InlineData("testemail")]
        [InlineData("test.email")]
        [InlineData("")]
        public void EmailShouldFailIfSetWithInvalidData(string p_input)
        {
            Customer _customerTest = new Customer();
            Assert.Throws<Exception>(() => _customerTest.Email = p_input);
        }

            // ---------- PhoneNumber ----------

        /// <summary>
        /// Will test if PhoneNumber property gives an exception if given valid data.
        /// valid data - only numbers and '()' or '-'
        /// </summary>
        [Theory]
        [InlineData("1231231234")]
        [InlineData("123-123-1234")]
        [InlineData("(123)123-1234")]
        public void PhoneNumberShouldSetValidData(string p_input)
        {   
            Customer _customerTest = new Customer();
            _customerTest.PhoneNumber = p_input;
            Assert.NotNull(_customerTest.PhoneNumber);
            Assert.Equal(_customerTest.PhoneNumber, p_input);
        }

        /// <summary>
        /// Will test if PhoneNumber property gives an exception if given invalid data.
        /// invalid data - letters or empty string.
        /// </summary>
        [Theory]
        [InlineData("test")]
        [InlineData("test 123")]
        [InlineData("")]
        public void PhoneNumberShouldFailIfSetWithInvalidData(string p_input)
        {
            Customer _customerTest = new Customer();
            Assert.Throws<Exception>(() => _customerTest.PhoneNumber = p_input);
        }

            // ---------- Orders ----------
        
        [Fact]
        public void OrdersShouldSetValidData()
        {
            Customer _customerTest = new Customer();
            Orders _order = new Orders();
            
            _customerTest.Orders.Add(_order);
            Assert.NotNull(_customerTest.Orders);
            Assert.Equal(_customerTest.Orders[0], _order);
        }
    }
}
