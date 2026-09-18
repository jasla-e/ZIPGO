using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {

        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCart(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }
        }

public async Task<Cart?> GetByUserId(int userId)
        {
            return await _context.Carts
               .Include(c => c.CartItems)
               .ThenInclude(ci => ci.Product)
               .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
