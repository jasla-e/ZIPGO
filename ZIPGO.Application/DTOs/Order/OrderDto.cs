using System;
using System.Collections.Generic;
using System.Text;
namespace ZIPGO.Application.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AddressId { get; set; }

        public string Status { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
            = new List<OrderItemDto>();
    }
}