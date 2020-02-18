using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnyCompany.Interfaces;
using AnyCompany.Models;
using AnyCompany.Service;
using Moq;
using Xunit;

namespace AnyCompany.Tests
{
    public class CustomersServiceTests
    {
        [Fact]
        public async Task Get_Customers_With_OrdersAsync()
        {
            IList<Order> orders = new List<Order>()
            {
                new Order(1, 10, 0.1),
                new Order(2, 20, 0.2),
                new Order(3, 30, 0.3),
                new Order(4, 40, 0.4),
                new Order(5, 50, 0.5)
            };
            var customers = new List<Customer>()
            {
                new Customer(1, "UK", DateTime.Now, "test"),
                new Customer(1, "UK", DateTime.Now, "test"),
                new Customer(1, "UK", DateTime.Now, "test")
            };
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockOrderRepo.Setup(or => or.GetCustomerOrdersAsync(It.IsAny<int>())).ReturnsAsync(orders);
            mockCustomerRepo.Setup(cr => cr.GetAllCustomersAsync()).ReturnsAsync(customers);
            var customerService = new CustomerService(mockOrderRepo.Object, mockCustomerRepo.Object);

            var resultCustomers = await customerService.GetCustomersAsync();

            Assert.Equal(customers.Count, resultCustomers.Count);
            foreach (var cust in resultCustomers)
            {
                Assert.Equal(orders.Count, cust.Orders.Count);
            }
        }
    }
}
