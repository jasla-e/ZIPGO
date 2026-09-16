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

        public string Status { get; set; }

        public DateTime OrderDate { get; set; }

        public Decimal TOtalAmount { get; set; }

    }
}
