using System;
using System.Collections.Generic;
using Data_Access_Logic;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;

namespace MMTest
{
    public class RepositoryTest
    {
        private readonly DbContextOptions<MMDBContext> _options;
        public RepositoryTest()
        {
            _options = new DbContextOptionsBuilder<MMDBContext>()
                            .UseSqlite("Filename = Test.db").Options;
            Seed();
        }

        [Fact]
        public void GetCustomerListShouldReturnAllCustomer()
        {
            using (var context = new MMDBContext(_options))
            {
                //Arrange
                IRepository repo = new RepositoryCloud(context);

                //Act
                var test = repo.GetCustomerList();

                //Assert
                Assert.Equal(2, test.Count);
                Assert.Equal("Colin", test[0].Name);
            }
        }

        [Fact]
        public void AddCustomerShouldAddCustomer()
        {
            using (var context = new MMDBContext(_options))
            {
                //Arrange
                IRepository repo = new RepositoryCloud(context);
                Customer _customerTest = new Customer
                {
                    Name = "Mike",
                    Address = "somewhere place",
                    Email = "mike@test.com",
                    PhoneNumber = "1113335555",
                    Id = "3",
                    Order = new List<Order>
                        {
                            new Order
                            {
                                Address = "store 1 place",
                                TotalPrice = 100,
                                StoreFrontId = 1,
                                CustomerId = "3"
                            },
                            new Order
                            {
                                Address = "store 2 place",
                                TotalPrice = 20,
                                StoreFrontId = 2,
                                CustomerId = "3"
                            }
                        }
                };

                //Act
                repo.AddCustomer(_customerTest);

                //Assert
                // needs to close the connection from earlier.
                using (var contexts = new MMDBContext(_options))
                {
                    var result = contexts.Customers.Find("3");
                    Assert.NotNull(result);

                }
            }
        }

        //public void AddLineItemsListToOrdersListShouldAddListOfLineItemsToOrders()
        //{

        //}

        [Fact]
        public void GetAllProductsShouldReturnAllProducts()
        {
            using (var context = new MMDBContext(_options))
            {
                IRepository repo = new RepositoryCloud(context);

                var test = repo.GetAllProducts();

                Assert.Equal(8, test.Count);
                Assert.Equal("prod name 1", test[0].Name);
            }
        }

        [Fact]
        public void GetLastOrderIdShouldReturnIdForLastIndexOfOrdersTable()
        {
            using (var context = new MMDBContext(_options))
            {
                IRepository repo = new RepositoryCloud(context);

                var test = repo.GetLastOrderId();

                Assert.Equal(8, test);
            }
        }

        [Fact]
        public void GetLineItemsListShouldReturnListOfAllLineItems()
        {
            using (var context = new MMDBContext(_options))
            {
                //Arrange
                IRepository repo = new RepositoryCloud(context);
                int _testStoreID = 1;
                //Act
                var test = repo.GetLineItemsList(_testStoreID);

                //Assert
                Assert.Equal(4, test.Count);
                Assert.Equal(7, test[0].ProductId);
            }
        }

        [Fact]
        public void GetOrdersListShouldReturnListOfOrdersFromStore()
        {
            using (var context = new MMDBContext(_options))
            {
                //Arrange
                IRepository repo = new RepositoryCloud(context);
                int _storeId = 1;

                //Act
                var testStore = repo.GetOrdersListForStore(_storeId);

                //Assert
                Assert.Equal(3, testStore.Count);
            }
        }

        [Fact]
        public void GetOrdersListShouldReturnListOfOrdersFromCustomer()
        {
            using (var context = new MMDBContext(_options))
            {
                //Arrange
                IRepository repo = new RepositoryCloud(context);
                string _custId = "1";

                //Act
                var testCustomer = repo.GetOrdersListForCustomer(_custId);

                //Assert
                Assert.Equal(6, testCustomer.Count);
            }
        }

        [Fact]
        public void GetLineItemByIdShouldReturnLineItemWithCorrectId()
        {
            using (var context = new MMDBContext(_options))
            {
                //Arrange
                IRepository repo = new RepositoryCloud(context);
                int _lineItemId = 1;

                //Act
                var itemFound = repo.GetLineItemsById(_lineItemId);

                //Assert
                Assert.Equal(1, itemFound.LineItemsId);
                Assert.Equal("prod name 3", itemFound.Product.Name);
            }
        }

        [Fact]
        public void GetStoreFrontByIdShouldReturnStoreFrontWithCorrectId()
        {
            using (var context = new MMDBContext(_options))
            {
                //Arrange
                IRepository repo = new RepositoryCloud(context);
                int _storeFrontId = 1;

                //Act
                StoreFront storeFound = repo.GetStoreFrontById(_storeFrontId);

                //Assert
                Assert.Equal(1, storeFound.StoreFrontId);
                Assert.Equal("Test Store 1", storeFound.Name);
            }
        }

        private void Seed()
        {
            using (var context = new MMDBContext(_options))
            {
                // ensures the the inmemory database is deleted and then recreated without any data from a previous test.
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Products.AddRange
                (
                    new Product
                    {
                        ProductId = 1,
                        Name = "prod name 1",
                        Price = 20,
                        Description = "a very nice product 1",
                        Brand = "Prod's Products",
                        Category = "really great prod"
                    },
                    new Product
                    {
                        ProductId = 2,
                        Name = "prod name 2",
                        Price = 12,
                        Description = "an even better product 2",
                        Brand = "Prod's Products",
                        Category = "really great prod"
                    }
                );


                context.Storefronts.AddRange
                (
                    new StoreFront
                    {
                        StoreFrontId = 1,
                        Name = "Test Store 1",
                        Address = "123 Store Ln",
                        LineItems = new List<LineItems>
                            {
                                new LineItems
                                {
                                    StoreFrontId = 1,
                                    LineItemsId = 3,
                                    ProductId = 1,
                                    Quantity = 2,
                                    Product = {
                                        Name = "prod name 1",
                                        Price = 20,
                                        Description = "a very nice product 1",
                                        Brand = "Prod's Products",
                                        Category = "really great prod"
                                    }
                                },
                                new LineItems
                                {
                                    StoreFrontId = 1,
                                    LineItemsId = 4,
                                    ProductId = 2,
                                    Quantity = 2,
                                    Product = {
                                        Name = "prod name 2",
                                        Price = 12,
                                        Description = "an even better product 2",
                                        Brand = "Prod's Products",
                                        Category = "really great prod"
                                    }
                                }
                            },
                        Order = new List<Order>
                            {
                                new Order {
                                    Address = "store 1 place",
                                    TotalPrice = 100,
                                    StoreFrontId = 1,
                                    CustomerId = "1"
                                },
                                new Order {
                                    Address = "store 2 place",
                                    TotalPrice = 20,
                                    StoreFrontId = 2,
                                    CustomerId = "1"
                                }
                            }
                    },
                    new StoreFront
                    {
                        StoreFrontId = 2,
                        Name = "Test Store 2",
                        Address = "456 2tore place",
                        LineItems = new List<LineItems>
                            {
                                new LineItems
                                {
                                    StoreFrontId = 2,
                                    LineItemsId = 5,
                                    ProductId = 1,
                                    Quantity = 2,
                                    Product = {
                                        Name = "prod name 1",
                                        Price = 20,
                                        Description = "a very nice product 1",
                                        Brand = "Prod's Products",
                                        Category = "really great prod"
                                    }
                                },
                                new LineItems
                                {
                                    StoreFrontId = 2,
                                    LineItemsId = 6,
                                    ProductId = 2,
                                    Quantity = 2,
                                    Product = {
                                        Name = "prod name 2",
                                        Price = 12,
                                        Description = "an even better product 2",
                                        Brand = "Prod's Products",
                                        Category = "really great prod"
                                    }
                                }
                            },
                        Order = new List<Order>
                            {
                                new Order {
                                    Address = "store 1 place",
                                    TotalPrice = 100,
                                    StoreFrontId = 1,
                                    CustomerId = "1"
                                },
                                new Order {
                                    Address = "store 2 place",
                                    TotalPrice = 20,
                                    StoreFrontId = 1,
                                    CustomerId = "1"
                                }
                            }
                    }
                );


                context.LineItems.AddRange
                (
                    new LineItems
                    {
                        StoreFrontId = 1,
                        LineItemsId = 1,
                        ProductId = 1,
                        Quantity = 7,
                        Product = new Product {
                            Name = "prod name 3",
                            Price = 20,
                            Description = "a very nice product 1",
                            Brand = "Prod's Products",
                            Category = "really great prod"
                        }
                    },
                    new LineItems
                    {
                        StoreFrontId = 1,
                        LineItemsId = 2,
                        ProductId = 2,
                        Quantity = 2,
                        Product = new Product {
                            Name = "prod name 4",
                            Price = 12,
                            Description = "an even better product 2",
                            Brand = "Prod's Products",
                            Category = "really great prod"
                        }
                    }
                );

                context.Customers.AddRange
                (
                    new Customer
                    {
                        Name = "Colin",
                        Address = "112 Streets st.",
                        Email = "colin@example.com",
                        PhoneNumber = "123-123-1234",
                        Id = "1",
                        Order = new List<Order>
                            {
                                new Order {
                                    Address = "store 1 place",
                                    TotalPrice = 100,
                                    StoreFrontId = 1,
                                    CustomerId = "1"
                                },
                                new Order {
                                    Address = "store 2 place",
                                    TotalPrice = 20,
                                    StoreFrontId = 2,
                                    CustomerId = "1"
                                }
                            }
                    },
                    new Customer
                    {
                        Name = "Kai",
                        Address = "12 bonneyview",
                        Email = "kai@example.com",
                        PhoneNumber = "989-123-1234",
                        Id = "2",
                        Order = new List<Order>
                            {
                                new Order {
                                    Address = "store 1 place",
                                    TotalPrice = 200,
                                    StoreFrontId = 2,
                                    CustomerId = "2"
                                },
                                new Order {
                                    Address = "store 2 place",
                                    TotalPrice = 40,
                                    StoreFrontId = 2,
                                    CustomerId = "2"
                                }
                            }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}