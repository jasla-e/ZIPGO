using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminOrderController:ControllerBase
    {
        private readonly IOrderService _orderService;

        public AdminOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAll();

            return Ok(orders);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchOrders(
    string search = "",
    string status = "")
        {
            var orders = await _orderService.SearchOrders(search, status);

            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(
    int id,
    string status)
        {
            await _orderService.UpdateStatus(id, status);

            return Ok("Order status updated successfully");
        }
    }
}
