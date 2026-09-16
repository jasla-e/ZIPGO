using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
    internal class CartItem
    {
        public int Id { get; set; }

        public int cartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
