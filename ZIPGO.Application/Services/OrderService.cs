using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs;
using ZIPGO.Application.DTOs.Order;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IAddressRepository _addressRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IAddressRepository addressRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _addressRepository = addressRepository;
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

        public async Task<OrderDto?> GetById(int id)
        {
            var order = await _orderRepository.GetById(id);

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

        public async Task UpdateStatus(int id, string status)
        {
            var order = await _orderRepository.GetById(id);

            if (order == null)
                return;

            order.Status = status;

            await _orderRepository.Update(order);
        }
        public async Task<OrderDto> CreateOrder(
            int userId,
            CreateOrderDto orderDto)
        {
            var address = await _addressRepository.GetById(orderDto.AddressId);

            if (address == null || address.UserId != userId)
            {
                throw new Exception("Invalid address");
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

            await _cartRepository.ClearCart(userId);

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

    }
}