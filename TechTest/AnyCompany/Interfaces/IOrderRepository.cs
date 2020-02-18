using System.Collections.Generic;
using System.Threading.Tasks;
using AnyCompany.Models;

namespace AnyCompany.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
        Task<IList<Order>> GetCustomerOrdersAsync(int customerId);
    }
}
