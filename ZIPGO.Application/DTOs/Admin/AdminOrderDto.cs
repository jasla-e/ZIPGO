using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Admin
{
    public class AdminOrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public List<AdminOrderItemDto> OrderItems { get; set; }
            = new List<AdminOrderItemDto>();
    }
}
