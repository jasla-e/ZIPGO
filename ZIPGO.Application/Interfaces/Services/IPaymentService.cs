using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Payment?> GetByOrderId(int orderId);

        Task Add(Payment payment);

        Task Update(Payment payment);

        Task CreatePayment(int userId,int orderId,string paymentMethod);
        Task VerifyPayment(int userId,int orderId,string razorpayOrderId,string razorpayPaymentId,string razorpaySignature);

        Task<string> CreateRazorpayOrder( int userId, int orderId);
    }
}