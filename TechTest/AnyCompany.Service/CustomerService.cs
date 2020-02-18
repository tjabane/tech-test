using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Interfaces;
using AnyCompany.Models;

namespace AnyCompany.Service
{
    public class CustomerService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            var customerWithOrders = customers.Select(async customer => await MapOrdersAsync(customer));
            var results = await Task.WhenAll(customerWithOrders);
            return results.ToList();
        }

        private async Task<Customer> MapOrdersAsync(Customer customer)
        {
            customer.Orders = await _orderRepository.GetCustomerOrdersAsync(customer.CustomerId);
            return customer;
        }

    }
}
