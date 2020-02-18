using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AnyCompany.Interfaces;
using AnyCompany.Models;

namespace AnyCompany.Infrastructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private static readonly string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public async Task SaveAsync(Order order)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)";
                command.Parameters.AddWithValue("@OrderId", order.OrderId);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@VAT", order.Vat);
                command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                await command.ExecuteNonQueryAsync();
                connection.Close();
            }
        }

        public async Task<IList<Order>> GetCustomerOrdersAsync(int customerId)
        {
            var orders = new List<Order>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Orders WHERE CustomerId = @CustomerId;";
                command.Parameters.AddWithValue("@CustomerId", customerId);

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var orderId = reader.GetInt32(reader.GetOrdinal("OrderId"));
                    var amount = reader.GetDouble(reader.GetOrdinal("Amount"));
                    var vat = reader.GetDouble(reader.GetOrdinal("VAT"));
                    var order = new Order(orderId, amount, vat)
                    {
                        CustomerId = customerId
                    };
                    orders.Add(order);
                }
                connection.Close();
            }
            return orders;
        }
    }
}
