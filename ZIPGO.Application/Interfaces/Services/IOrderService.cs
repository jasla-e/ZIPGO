using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs;
using ZIPGO.Application.DTOs.Order;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAll();

        Task<OrderDto?> GetById(int id);

        Task<List<OrderDto>> GetMyOrders(int userId);

        Task UpdateStatus(int id, string status);

        Task<OrderDto> CreateOrder(
            int userId,
            CreateOrderDto orderDto);
    }
}