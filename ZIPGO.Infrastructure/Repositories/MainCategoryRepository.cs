using Microsoft.EntityFrameworkCore;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class MainCategoryRepository : IMainCategoryRepository
    {
        private readonly AppDbContext _context;

        public MainCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MainCategory>> GetAll()
        {
            return await _context.MainCategories
                .ToListAsync();
        }

        public async Task<MainCategory?> GetById(int id)
        {
            return await _context.MainCategories
                .FindAsync(id);
        }
    }
}