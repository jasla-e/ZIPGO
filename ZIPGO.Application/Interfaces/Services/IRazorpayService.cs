using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IRazorpayService
    {
        Task<string> CreateOrder(decimal amount, int orderId);

        Task<bool> VerifyPayment(
            string razorpayOrderId,
            string razorpayPaymentId,
            string razorpaySignature);
    }
}