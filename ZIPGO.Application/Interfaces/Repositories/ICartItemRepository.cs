using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetById(int id);

        Task<CartItem?> GetByCartAndProduct(
            int cartId,
            int productId);

        Task Add(CartItem cartItem);

        Task Update(CartItem cartItem);

        Task Remove(int id);
    }
}