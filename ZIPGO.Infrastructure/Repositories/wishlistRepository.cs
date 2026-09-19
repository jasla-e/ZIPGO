using Microsoft.EntityFrameworkCore;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AppDbContext _context;

        public WishlistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Wishlist?> GetByUserId(int userId)
        {
            return await _context.Wishlists
                .Include(w => w.WishlistItems)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task Add(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var wishlist = await _context.Wishlists.FindAsync(id);

            if (wishlist != null)
            {
                _context.Wishlists.Remove(wishlist);
                await _context.SaveChangesAsync();
            }
        }
    }
}