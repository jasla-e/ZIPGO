using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
    public class Payment
    {

        public int Id { get; set; }

        public int OrderId { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public string TransactionId { get; set; }

        public decimal Amount { get; set; }

    }
}
