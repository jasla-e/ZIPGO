using System;
using System.Collections.Generic;
using System.Text;
namespace ZIPGO.Application.DTOs.Order
{
    public class CreateOrderResponseDto
    {
        public OrderDto Order { get; set; } = null!;

        public string? RazorpayOrderId { get; set; }

        public string? RazorpayKeyId { get; set; }
    }
}