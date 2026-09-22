using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();

        Task<Order?> GetById(int id, int userId);

        Task<List<Order>> GetByUserId(int userId);

        Task Add(Order order);

        Task Update(Order order);
    }
}