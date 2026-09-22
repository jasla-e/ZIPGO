using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs;
using ZIPGO.Application.DTOs.Order;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Application.Settings;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRazorpayService _razorpayService;
        private readonly RazorpaySettings _razorpaySettings;

        public OrderService(
         IOrderRepository orderRepository,
         ICartRepository cartRepository,
         IAddressRepository addressRepository,
         IPaymentRepository paymentRepository,
         IRazorpayService razorpayService,
        IOptions<RazorpaySettings> razorpaySettings)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _addressRepository = addressRepository;
            _paymentRepository = paymentRepository;
            _razorpayService = razorpayService;
            _razorpaySettings = razorpaySettings.Value;
        }
        public async Task<List<OrderDto>> GetAll()
        {
            var orders = await _orderRepository.GetAll();

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                AddressId = order.AddressId,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,

                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto?> GetById(int id, int userId)
        {
            var order = await _orderRepository.GetById(id, userId);

            if (order == null)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                AddressId = order.AddressId,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,

                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }

        public async Task<List<OrderDto>> GetMyOrders(int userId)
        {
            var orders = await _orderRepository.GetByUserId(userId);

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                AddressId = order.AddressId,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,

                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            }).ToList();
        }

        public async Task<CreateOrderResponseDto> CreateOrder(
            int userId,
            CreateOrderDto orderDto)
        {
            var address = await _addressRepository.GetById(orderDto.AddressId);

            if (address == null || address.UserId != userId)
            {
                throw new Exception("Invalid address");
            }

            if (orderDto.PaymentMethod != "COD" &&
            orderDto.PaymentMethod != "UPI")
            {
                throw new Exception("Invalid payment method");
            }

            var cart = await _cartRepository.GetByUserId(userId);

            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart is empty");
            }

            var order = new Order
            {
                UserId = userId,
                AddressId = orderDto.AddressId,
                Status = "Pending",
                OrderDate = DateTime.UtcNow,
                TotalAmount = 0
            };

            foreach (var cartItem in cart.CartItems)
            {
                if (cartItem.Quantity > cartItem.Product.Stock)
                    throw new Exception(
                        $"Not enough stock for {cartItem.Product.Name}");

                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price
                };

                order.OrderItems.Add(orderItem);

                order.TotalAmount +=
                    cartItem.Product.Price * cartItem.Quantity;
            }

            await _orderRepository.Add(order);

            string? razorpayOrderId = null;
            string? razorpayKeyId = _razorpaySettings.KeyId;

            if (orderDto.PaymentMethod == "COD")
            {
                var payment = new Payment
                {
                    OrderId = order.Id,
                    PaymentMethod = "COD",
                    PaymentStatus = "Paid",
                    TransactionId = "COD",
                    Amount = order.TotalAmount
                };

                await _paymentRepository.Add(payment);

                await _cartRepository.ClearCart(userId);
            }
            else if (orderDto.PaymentMethod == "UPI")
            {
                razorpayOrderId = await _razorpayService.CreateOrder(
                    order.TotalAmount,
                    order.Id);

                var payment = new Payment
                {
                    OrderId = order.Id,
                    PaymentMethod = "UPI",
                    PaymentStatus = "Pending",
                    Amount = order.TotalAmount,
                    RazorpayOrderId = razorpayOrderId
                };

                await _paymentRepository.Add(payment);
            }
           

            return new CreateOrderResponseDto
            {
                Order = new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    AddressId = order.AddressId,
                    Status = order.Status,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,

                    OrderItems = order.OrderItems.Select(item => new OrderItemDto
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    }).ToList()
                },

                RazorpayOrderId = razorpayOrderId,
                RazorpayKeyId = razorpayKeyId
            };
        }
    }
}