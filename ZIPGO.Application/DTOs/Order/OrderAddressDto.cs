using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Order
{
    public class OrderAddressDto
    {
        public string FullName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string House { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string Pincode { get; set; } = null!;
    }
}