using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs.Admin;

namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface IDasboardRepository
    {

        Task<int> GetTotalProducts();

         Task<int> GetTotalUsers();

        Task<int> GetTotalOrders();

        Task<decimal> GetYearlyRevenue();
        Task<List<MonthlyRevenueDto>> GetMonthlyRevenue();

        Task<List<OrderStatusDto>> GetOrderStatus();

        Task<List<CategorySalesDto>> GetCategorySales();

        Task<List<TopProductDto>> GetTopProducts(int? month);

        Task<List<RecentOrderDto>> GetRecentOrders();
    }
}
