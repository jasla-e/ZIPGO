using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private int GetUserId()
        {
            return int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = GetUserId();

            var cart = await _cartService.GetCart(userId);

            if (cart == null)
                return NotFound("Cart not found");

            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem(
            int productId,
            int quantity)
        {
            var userId = GetUserId();

            await _cartService.AddItem(
                userId,
                productId,
                quantity);

            return Ok("Item added to cart");
        }

        [HttpPut("items/{id}")]
        public async Task<IActionResult> UpdateItem(
            int id,
            int quantity)
        {
            var userId = GetUserId();

            await _cartService.UpdateItem(
                userId,
                id,
                quantity);

            return Ok("Cart item updated");
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var userId = GetUserId();

            await _cartService.RemoveItem(
                userId,
                id);

            return Ok("Item removed from cart");
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();

            await _cartService.ClearCart(userId);

            return Ok("Cart cleared");
        }
    }
}