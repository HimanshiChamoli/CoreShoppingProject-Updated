using System;
using System.Collections.Generic;

namespace EcommerceUserPanel.Models
{
    public partial class Payments
    {
        public int PaymentId { get; set; }
        public string PaymentStripeId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public double CardNo { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }

        public Orders Order { get; set; }
    }
}
