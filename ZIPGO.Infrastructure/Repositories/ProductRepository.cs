using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.Interfaces;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Product = await _context.Products.FindAsync(id);

            if (Product != null) { 
             _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            
            }
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Update(Product product)
        {
            var existingProduct=await _context.Products.FindAsync(product.Id);
            if (existingProduct != null) {

                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.Rating = product.Rating;
                existingProduct.Image = product.Image;
                existingProduct.Offer = product.Offer;
                existingProduct.SubCategoryId = product.SubCategoryId;


            await _context.SaveChangesAsync();

            }
        }
    }
}
