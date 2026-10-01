using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Admin
{
    public class RecentOrderDto
    {
        public int OrderId { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }
    }
}