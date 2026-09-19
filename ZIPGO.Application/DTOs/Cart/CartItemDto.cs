using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Cart
{
    public class CartItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}