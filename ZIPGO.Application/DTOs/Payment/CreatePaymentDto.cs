using System;
using System.Collections.Generic;
using System.Text;
namespace ZIPGO.Application.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = null!;
    }
}