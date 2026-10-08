using Microsoft.EntityFrameworkCore;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _context;

        public SubCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubCategory>> GetAll()
        {
            return await _context.SubCategories
                .Include(s => s.MainCategories)
                .ToListAsync();
        }

        public async Task<List<SubCategory>> GetByMainCategoryId(
            int mainCategoryId)
        {
            return await _context.SubCategories
                .Include(s => s.MainCategories)
                .Where(s => s.MainCategories
                    .Any(m => m.Id == mainCategoryId))
                .ToListAsync();
        }

        public async Task<SubCategory?> GetById(int id)
        {
            return await _context.SubCategories
                .Include(s => s.MainCategories)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}