using AnyCompany.Interfaces;
using AnyCompany.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnyCompany.Infrastructure.Repositories
{
    public class CustomerRepositoryWrapper : ICustomerRepository
    {
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() => await CustomerRepository.LoadAllCustomersAsync();

        public async Task<Customer> GetCustomerAsync(int customerId) => await CustomerRepository.LoadAsync(customerId);
    }
}
