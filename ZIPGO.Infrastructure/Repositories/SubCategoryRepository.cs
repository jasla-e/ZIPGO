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
              .ToListAsync();
        }

        public async Task<SubCategory?> GetById(int id)
        {
            return await _context.SubCategories
                 .FindAsync(id);
        }

        public async Task Update(SubCategory subCategory)
        {
            var existingSubCategory = await _context.SubCategories
                 .FindAsync(subCategory.Id);

            if (existingSubCategory != null)
            {
                existingSubCategory.Name = subCategory.Name;
                existingSubCategory.MainCategoryId = subCategory.MainCategoryId;

                await _context.SaveChangesAsync();
            }
        }
    }
}
