using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminDashboardController:ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public AdminDashboardController(IDashboardService dashboardService) { 
         _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var dashboard = await _dashboardService.GetDashBoard();

            return Ok(dashboard);
        }

        [HttpGet("top-products")]
        public async Task<IActionResult> GetTopProducts(int? month)
        {
            var topProducts = await _dashboardService.GetTopProducts(month);

            return Ok(topProducts);
        }

    }
}
