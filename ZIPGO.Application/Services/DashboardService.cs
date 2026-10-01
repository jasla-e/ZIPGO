using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs.Admin;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.Application.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly IDasboardRepository _dasboardRepository;

        public DashboardService(IDasboardRepository dasboardRepository)
        {
            _dasboardRepository = dasboardRepository;
        }



        public async Task<DashboardDto> GetDashBoard()
        {
            var totalProducts = await _dasboardRepository.GetTotalProducts();
            var totalUsers = await _dasboardRepository.GetTotalUsers();
            var totalOrders = await _dasboardRepository.GetTotalOrders();
            var yearlyRevenue = await _dasboardRepository.GetYearlyRevenue();
            var monthlyRevenue = await _dasboardRepository.GetMonthlyRevenue();
            var orderStatus = await _dasboardRepository.GetOrderStatus();
            var CategorySales = await _dasboardRepository.GetCategorySales();
            var TopProducts = await  _dasboardRepository.GetTopProducts(null);
            var recentOrders = await _dasboardRepository.GetRecentOrders();

            return new DashboardDto
            {
                TotalProducts = totalProducts,
                TotalUsers = totalUsers,
                TotalOrders = totalOrders,
                YearlyRevenue = yearlyRevenue,
                MonthlyRevenue = monthlyRevenue,
                OrderStatus = orderStatus,
                CategorySales=CategorySales,
                TopProducts = TopProducts,
                RecentOrders = recentOrders
            };
        }


        public async Task<List<TopProductDto>> GetTopProducts(int? month)
        {
            return await _dasboardRepository.GetTopProducts(month);
        }


    }
}
