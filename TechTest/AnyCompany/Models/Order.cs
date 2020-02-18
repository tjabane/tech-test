using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Models
{
    public class Order
    {
        public Order(int orderId, double amount, double vat)
        {
            OrderId = orderId;
            Amount = amount;
            Vat = vat;
        }

        public int OrderId { get; set; }
        public double Amount { get; set; }
        public double Vat { get; set; }

        public int CustomerId { get; set; }
    }
}
