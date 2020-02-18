using System.Threading.Tasks;
using AnyCompany.Interfaces;
using AnyCompany.Models;

namespace AnyCompany.Service
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<bool> PlaceOrderAsync(Order order, int customerId)
        {
            var customer = await _customerRepository.GetCustomerAsync(customerId);

            if (customer == null)
                return false;

            if (order.Amount.Equals(0))
                return false;

            if (customer.Country == "UK")
                order.Vat = 0.2d;
            else
                order.Vat = 0;

            order.CustomerId = customer.CustomerId;

            await _orderRepository.SaveAsync(order);

            return true;
        }

    }
}
