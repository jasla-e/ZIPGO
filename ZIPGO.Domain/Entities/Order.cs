using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AddressId { get; set; }

        public string Status { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public User User { get; set; } = null!;

        public Address Address { get; set; } = null!;

        public Payment Payment { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();
    }
}