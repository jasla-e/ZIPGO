using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs.Admin;

namespace ZIPGO.Application.Interfaces.Services
{
   public interface IDashboardService
    {

        Task<DashboardDto> GetDashBoard();

        Task<List<TopProductDto>> GetTopProducts(int? month);

    }
}
