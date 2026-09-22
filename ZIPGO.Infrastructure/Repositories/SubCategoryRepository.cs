using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ZIPGO.Infrastructure.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {


        private readonly AppDbContext _context;
        public SubCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var subCategory = await _context.SubCategories
               .FindAsync(id);

            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<SubCategory>> GetAll()
        {
            return await _context.SubCategories
                .Include(s => s.MainCategories)
                .ToListAsync();
        }

        public async Task<List<SubCategory>> GetByMainCategoryId(int mainCategoryId)
        {
            return await _context.SubCategories
                .Include(s => s.MainCategories)
                .Where(s => s.MainCategories.Any(m => m.Id == mainCategoryId))
                .ToListAsync();
        }
        public async Task<bool> BelongsToMainCategory(
          int subCategoryId,
          int mainCategoryId)
        {
            return await _context.SubCategories
                .AnyAsync(s =>
                    s.Id == subCategoryId &&
                    s.MainCategories.Any(m => m.Id == mainCategoryId));
        }
        public async Task<SubCategory?> GetById(int id)
        {
            return await _context.SubCategories
                .Include(s => s.MainCategories)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Update(SubCategory subCategory)
        {
            await _context.SaveChangesAsync();
        }
    }
}
