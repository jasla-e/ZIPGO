using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Admin
{
    public class DashboardDto
    {
        public int TotalProducts { get; set; }
        public int TotalUsers { get; set; }
        public int TotalOrders { get; set; }
        public decimal YearlyRevenue { get; set; }

     public List<MonthlyRevenueDto> MonthlyRevenue { get; set; }
      = new List<MonthlyRevenueDto>();

        public List<OrderStatusDto> OrderStatus { get; set; }
           = new List<OrderStatusDto>();

        public List<CategorySalesDto> CategorySales { get; set; }
           = new List<CategorySalesDto>();

        public List<TopProductDto> TopProducts { get; set; }
        = new List<TopProductDto>();
        public List<RecentOrderDto> RecentOrders { get; set; }
        = new List<RecentOrderDto>();
    }
}
