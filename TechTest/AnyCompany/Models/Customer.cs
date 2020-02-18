using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public Customer(int customerId, string country, DateTime dateOfBirth, string name) : this()
        {
            CustomerId = customerId;
            Country = country;
            DateOfBirth = dateOfBirth;
            Name = name;
        }

        public int CustomerId { get; set; }
        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
