using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByOrderId(int orderId);

        Task Add(Payment payment);

        Task Update(Payment payment);
    }
}