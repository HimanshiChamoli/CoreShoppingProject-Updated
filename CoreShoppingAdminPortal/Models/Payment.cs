using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShoppingAdminPortal.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentStripeId { get; set; }
        public int amount { get; set; }
        public DateTime Date { get; set; }
        public double Card_no { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
