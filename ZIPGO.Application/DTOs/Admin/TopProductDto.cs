using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Admin
{
    public class TopProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int QuantitySold { get; set; }
        public int AvailableStock { get; set; }
    
    }
}
