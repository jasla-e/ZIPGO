using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs;
using ZIPGO.Application.DTOs.Admin;
using ZIPGO.Application.DTOs.Order;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto?> GetById(int id, int userId);

        Task<List<OrderDto>> GetMyOrders(int userId);

        Task<List<AdminOrderDto>> GetAll();

        Task<CreateOrderResponseDto> CreateOrder(
            int userId,
            CreateOrderDto orderDto);

        Task UpdateStatus(int orderId, string status);
        Task<List<AdminOrderDto>> SearchOrders(string search, string status);
    }
}