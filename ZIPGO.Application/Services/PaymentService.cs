using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRazorpayService _razorpayService;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public PaymentService(
       IPaymentRepository paymentRepository,
       IRazorpayService razorpayService,
       IOrderRepository orderRepository,
      ICartRepository cartRepository)
        {
            _paymentRepository = paymentRepository;
            _razorpayService = razorpayService;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }
        public async Task<Payment?> GetByOrderId(int orderId)
        {
            return await _paymentRepository.GetByOrderId(orderId);
        }

        public async Task Add(Payment payment)
        {
            await _paymentRepository.Add(payment);
        }

        public async Task Update(Payment payment)
        {
            await _paymentRepository.Update(payment);
        }

        public async Task CreatePayment(
   int userId,
   int orderId,
   string paymentMethod)
        {
            var order = await _orderRepository.GetById(orderId, userId);

            if (order == null || order.UserId != userId)
                throw new Exception("Invalid order");

            var existingPayment = await _paymentRepository.GetByOrderId(orderId);

            if (existingPayment != null)
                throw new Exception("Payment already exists for this order");

            if (paymentMethod != "COD" && paymentMethod != "UPI")
                throw new Exception("Invalid payment method");

            var payment = new Payment
            {
                OrderId = orderId,
                PaymentMethod = paymentMethod,
                PaymentStatus = "Pending",
                Amount = order.TotalAmount
            };

            await _paymentRepository.Add(payment);
        }

        public async Task<string> CreateRazorpayOrder(
    int userId,
    int orderId)
        {
            var order = await _orderRepository.GetById(orderId, userId);

            if (order == null || order.UserId != userId)
                throw new Exception("Invalid order");

            var payment = await _paymentRepository.GetByOrderId(orderId);

            if (payment == null)
                throw new Exception("Payment not created");

            if (payment.PaymentMethod != "UPI")
                throw new Exception("Invalid payment method");

            var razorpayOrderId = await _razorpayService.CreateOrder(
                order.TotalAmount,
                orderId);

            payment.RazorpayOrderId = razorpayOrderId;

            await _paymentRepository.Update(payment);

            return razorpayOrderId;
        }
        public async Task VerifyPayment(
        int userId,
        int orderId,
         string razorpayOrderId,
         string razorpayPaymentId,
         string razorpaySignature)
        {
            var order = await _orderRepository.GetById(orderId, userId);

            if (order == null || order.UserId != userId)
                throw new Exception("Invalid order");

            var payment = await _paymentRepository.GetByOrderId(orderId);

            if (payment == null)
                throw new Exception("Payment not found");

            if (payment.PaymentStatus == "Paid")
                throw new Exception("Payment already completed");

            if (payment.PaymentMethod != "UPI")
                throw new Exception("Invalid payment method");

            if (payment.RazorpayOrderId != razorpayOrderId)
                throw new Exception("Invalid Razorpay order");

            var isValid = await _razorpayService.VerifyPayment(
                razorpayOrderId,
                razorpayPaymentId,
                razorpaySignature);

            if (!isValid)
                throw new Exception("Payment verification failed");

            payment.PaymentStatus = "Paid";
            payment.TransactionId = razorpayPaymentId;
            payment.RazorpaySignature = razorpaySignature;

            await _paymentRepository.Update(payment);

            await _cartRepository.ClearCart(userId);
        }


    }
}