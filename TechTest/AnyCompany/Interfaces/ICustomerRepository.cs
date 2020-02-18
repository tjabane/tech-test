using System.Collections.Generic;
using System.Threading.Tasks;
using AnyCompany.Models;

namespace AnyCompany.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerAsync(int customerId);

        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
