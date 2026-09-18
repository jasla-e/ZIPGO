using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task Add(MainCategory mainCategory)
        {
            _context.MainCategories.Add(mainCategory);
            await _context.SaveChangesAsync();
        }

        public async Task Update(MainCategory mainCategory)
        {
            var existingMainCategory = await _context.MainCategories
                .FindAsync(mainCategory.Id);

            if (existingMainCategory != null)
            {
                existingMainCategory.Name = mainCategory.Name;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var mainCategory = await _context.MainCategories
                .FindAsync(id);

            if (mainCategory != null)
            {
                _context.MainCategories.Remove(mainCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}