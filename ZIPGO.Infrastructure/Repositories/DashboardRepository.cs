using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs.Admin;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class DashboardRepository : IDasboardRepository
    {

        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalOrders()
        {
         return await _context.Orders.CountAsync();   
        }

        public async Task<int> GetTotalProducts()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetTotalUsers()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<decimal> GetYearlyRevenue()
        {
        
            var startDate = new DateTime(DateTime.UtcNow.Year, 1, 1);
            var endDate = startDate.AddYears(1);

            return await _context.Orders
                .Where(o =>
                    o.Status == "Completed" &&
                    o.OrderDate >= startDate &&
                    o.OrderDate < endDate)
                .SumAsync(o => o.TotalAmount);
        
    }


        public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenue()
        {
            var currentYear = DateTime.Now.Year;

            return await _context.Orders
                .Where(o => o.OrderDate.Year == currentYear)
                .GroupBy(o => o.OrderDate.Month)
                .Select(g => new MonthlyRevenueDto
                {
                    Month = g.Key,
                    Revenue = g.Sum(o => o.TotalAmount)
                })
                .ToListAsync();
        }

        public async Task<List<OrderStatusDto>> GetOrderStatus()
        {
            return await _context.Orders
              .GroupBy(o => o.Status)
             .Select(g => new OrderStatusDto
             {
               Status = g.Key,
               Count = g.Count()
                 })
                .ToListAsync();
             }

        public async Task<List<CategorySalesDto>> GetCategorySales()
        {
            return await _context.OrderItems
       .Include(oi => oi.Product)
           .ThenInclude(p => p.MainCategory)
       .GroupBy(oi => oi.Product.MainCategory.Name)
       .Select(g => new CategorySalesDto
       {
           CategoryName = g.Key,
           Sales = g.Sum(oi => oi.Quantity * oi.Price)
       })
       .ToListAsync();
        }


        public async Task<List<TopProductDto>> GetTopProducts(int? month)
        {
            var currentYear = DateTime.UtcNow.Year;

            var startDate = new DateTime(currentYear, 1, 1);
            var endDate = startDate.AddYears(1);

            var query = _context.OrderItems
                .Where(oi =>
                    oi.Order.OrderDate >= startDate &&
                    oi.Order.OrderDate < endDate);

            if (month.HasValue)
            {
                var monthStart = new DateTime(currentYear, month.Value, 1);
                var monthEnd = monthStart.AddMonths(1);

                query = query.Where(oi =>
                    oi.Order.OrderDate >= monthStart &&
                    oi.Order.OrderDate < monthEnd);
            }

            return await query
                .GroupBy(oi => new
                {
                    oi.ProductId,
                    oi.Product.Name,
                    oi.Product.Stock
                })
                .Select(g => new TopProductDto
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    QuantitySold = g.Sum(oi => oi.Quantity),
                    AvailableStock = g.Key.Stock
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<RecentOrderDto>> GetRecentOrders()
        {
            return await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .Select(o => new RecentOrderDto
                {
                    OrderId = o.Id,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount
                })
                .ToListAsync();
        }

    }
}
