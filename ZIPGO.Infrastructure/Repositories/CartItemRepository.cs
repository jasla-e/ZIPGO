using Microsoft.EntityFrameworkCore;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;

        public CartItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem?> GetById(int id)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<CartItem?> GetByCartAndProduct(
            int cartId,
            int productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci =>
                    ci.CartId == cartId &&
                    ci.ProductId == productId);
        }

        public async Task Add(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}