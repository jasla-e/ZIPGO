using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class WishlistItemRepository : IWishlistItemRepository
    {
        private readonly AppDbContext _context;

        public WishlistItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WishlistItem?> GetById(int id)
        {
            return await _context.WishlistItems
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WishlistItem?> GetByWishlistAndProduct(
            int wishlistId,
            int productId)
        {
            return await _context.WishlistItems
                .FirstOrDefaultAsync(w =>
                    w.WishlistId == wishlistId &&
                    w.ProductId == productId);
        }

        public async Task Add(WishlistItem wishlistItem)
        {
            _context.WishlistItems.Add(wishlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var wishlistItem = await _context.WishlistItems.FindAsync(id);

            if (wishlistItem != null)
            {
                _context.WishlistItems.Remove(wishlistItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}