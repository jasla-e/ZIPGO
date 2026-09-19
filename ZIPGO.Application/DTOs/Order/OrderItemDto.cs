using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}