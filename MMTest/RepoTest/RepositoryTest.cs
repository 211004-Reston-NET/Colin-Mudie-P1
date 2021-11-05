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
                Assert.Equal(test.Count, 2);
                Assert.Equal("Colin", test[0].Name);
            }
        }

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
                    Order = new List<Order>
                        {
                            new Order {
                                Address = "store 1 place",
                                TotalPrice = 100,
                                StoreFrontId = 1,
                                CustomerId = 1
                            },
                            new Order {
                                Address = "store 2 place",
                                TotalPrice = 20,
                                StoreFrontId = 2,
                                CustomerId = 1
                            }
                        }
                };
                
                //Act
                repo.AddCustomer(_customerTest);

                //Assert
                // needs to close the connection from earlier.
                using (var contexts = new MMDBContext(_options))
                {
                    var result = contexts.Customers.Find(3);
                    Assert.NotNull(result);
                
                }
            }
        }

        private void Seed()
        {
            using (var context = new MMDBContext(_options))
            {
                // ensures the the inmemory database is deleted and then recreated without any data from a previous test.
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Storefronts.AddRange
                (
                    new StoreFront
                    {
                        Name = "Test Store 1",
                        Address = "123 Store Ln",
                        LineItems = new List<LineItems>
                            {
                                new LineItems
                                {
                                    Product = new Product
                                    {
                                        Name = "prod name 1",
                                        Price = 12,
                                        Description = "a very nice product 1",
                                        Brand = "Prod's Products",
                                        Category = "really great prod"
                                    },
                                    Quantity = 2
                                },
                                new LineItems
                                {
                                    Product = new Product
                                    {
                                        Name = "prod name 2",
                                        Price = 12,
                                        Description = "an even better product 2",
                                        Brand = "Prod's Products",
                                        Category = "really great prod"
                                    },
                                    Quantity = 2
                                }
                            },
                        Order = new List<Order>
                            {
                                new Order {
                                    Address = "store 1 place",
                                    TotalPrice = 100,
                                    StoreFrontId = 1,
                                    CustomerId = 1
                                },
                                new Order {
                                    Address = "store 2 place",
                                    TotalPrice = 20,
                                    StoreFrontId = 2,
                                    CustomerId = 1
                                }
                            }
                    },
                    new StoreFront
                    {
                        Name = "Test Store 2",
                        Address = "456 2tore place",
                        LineItems = new List<LineItems>
                            {
                                new LineItems
                                {
                                    Product = new Product
                                    {
                                        Name = "prod name 1",
                                        Price = 12,
                                        Description = "a very nice product 1",
                                        Brand = "Prod's Products",
                                        Category = "really great prod"
                                    },
                                    Quantity = 2
                                },
                                new LineItems
                                {
                                    Product = new Product
                                    {
                                        Name = "prod name 2",
                                        Price = 12,
                                        Description = "an even better product 2",
                                        Brand = "Prod's Products",
                                        Category = "really great prod"
                                    },
                                    Quantity = 2
                                }
                            },
                        Order = new List<Order>
                            {
                                new Order {
                                    Address = "store 1 place",
                                    TotalPrice = 100,
                                    StoreFrontId = 1,
                                    CustomerId = 1
                                },
                                new Order {
                                    Address = "store 2 place",
                                    TotalPrice = 20,
                                    StoreFrontId = 2,
                                    CustomerId = 1
                                }
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
                        Order = new List<Order>
                            {
                                new Order {
                                    Address = "store 1 place",
                                    TotalPrice = 100,
                                    StoreFrontId = 1,
                                    CustomerId = 1
                                },
                                new Order {
                                    Address = "store 2 place",
                                    TotalPrice = 20,
                                    StoreFrontId = 2,
                                    CustomerId = 1
                                }
                            }
                    },
                    new Customer
                    {
                        Name = "Kai",
                        Address = "12 bonneyview",
                        Email = "kai@example.com",
                        PhoneNumber = "989-123-1234",
                        Order = new List<Order>
                            {
                                new Order {
                                    Address = "store 1 place",
                                    TotalPrice = 200,
                                    StoreFrontId = 1,
                                    CustomerId = 2
                                },
                                new Order {
                                    Address = "store 2 place",
                                    TotalPrice = 40,
                                    StoreFrontId = 2,
                                    CustomerId = 2
                                }
                            }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}