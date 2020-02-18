using System.Threading.Tasks;
using AnyCompany.Interfaces;
using AnyCompany.Models;
using Moq;
using Xunit;

namespace AnyCompany.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task PlaceOrder_Successfully()
        {
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockOrderRepo.Setup(or => or.SaveAsync(It.Is<Order>(o => o == null))).Throws<System.NullReferenceException>();
            mockCustomerRepo.Setup(cr => cr.GetCustomerAsync(It.IsAny<int>())).Returns(Task.Run(() => new Customer()));
            var orderService = new Service.OrderService(mockOrderRepo.Object, mockCustomerRepo.Object);
            var order = new Order(1, 100, 0.15);

            var result = await orderService.PlaceOrderAsync(order, 1);

            Assert.True(result, "Should save successfully");
        }

        [Fact]
        public async Task Place_Order_With_No_Customer_Failure()
        {
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockOrderRepo.Setup(or => or.SaveAsync(It.IsAny<Order>()));
            mockCustomerRepo.Setup(cr => cr.GetCustomerAsync(It.Is<int>(id => id == -1)));
            var orderService = new Service.OrderService(mockOrderRepo.Object, mockCustomerRepo.Object);

            var order = new Order(1, 100, 0.15);
            var result = await orderService.PlaceOrderAsync(order, -1);

            Assert.False(result, "no customer provided, should fail");
        }


        [Fact]
        public async Task Place_Order_For_UK_Customer()
        {
            var ukCustomer = new Customer()
            {
                Country = "UK",
                CustomerId = 2
            };
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockOrderRepo.Setup(or => or.SaveAsync(It.IsAny<Order>()));
            mockCustomerRepo.Setup(cr => cr.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync(ukCustomer);
            var orderService = new Service.OrderService(mockOrderRepo.Object, mockCustomerRepo.Object);
            var order = new Order(1, 100, 0.15);

            var result = await orderService.PlaceOrderAsync(order, 2);

            Assert.True(result, "Should save successfully");
            Assert.Equal(0.2d, order.Vat);
        }


        [Fact]
        public async Task Place_Order_For_Non_UK_Customers()
        {
            var saCustomer = new Customer
            {
                Country = "SA",
                CustomerId = 3
            };
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockOrderRepo.Setup(or => or.SaveAsync(It.IsAny<Order>()));
            mockCustomerRepo.Setup(cr => cr.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync(saCustomer);
            var orderService = new Service.OrderService(mockOrderRepo.Object, mockCustomerRepo.Object);
            var order = new Order(1, 100, 0.15);

            var result = await orderService.PlaceOrderAsync(order, 3);

            Assert.True(result, "Should save successfully");
            Assert.Equal(0, order.Vat);
        }


        [Fact]
        public async Task Place_Order_With_No_Amount_Failure()
        {
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockOrderRepo.Setup(or => or.SaveAsync(It.IsAny<Order>()));
            mockCustomerRepo.Setup(cr => cr.GetCustomerAsync(It.IsAny<int>())).Returns(Task.Run(() => new Customer()));
            var orderService = new Service.OrderService(mockOrderRepo.Object, mockCustomerRepo.Object);
            var order = new Order(1, 0, 0.15);

            var result = await orderService.PlaceOrderAsync(order, 3);

            Assert.False(result, "Fail due to zero amount");

        }
    }
}
