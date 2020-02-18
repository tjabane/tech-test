using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AnyCompany.Models;

namespace AnyCompany.Infrastructure.Repositories
{
    internal static class CustomerRepository
    {
        private static readonly string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";


        internal static async Task<Customer> LoadAsync(int customerId)
        {
            var customer = new Customer();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Customer WHERE CustomerId = @CustomerId;";
                command.Parameters.AddWithValue("@CustomerId", customerId);

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    customer.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                    customer.Name = reader.GetString(reader.GetOrdinal("Name"));
                    customer.DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    customer.Country = reader.GetString(reader.GetOrdinal("Country"));
                }
                connection.Close();
            }
            return customer;
        }


        internal static async Task<IList<Customer>> LoadAllCustomersAsync()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Customer";

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Country = reader.GetString(reader.GetOrdinal("Country"))
                    };

                    customers.Add(customer);
                }
                connection.Close();
            }
            return customers;
        }
    }
}
