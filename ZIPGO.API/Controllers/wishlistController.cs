using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        private int GetUserId()
        {
            return int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlist()
        {
            var userId = GetUserId();

            var wishlist = await _wishlistService.GetWishlist(userId);

            if (wishlist == null)
                return NotFound("Wishlist not found");

            return Ok(wishlist);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem(int productId)
        {
            var userId = GetUserId();

            await _wishlistService.AddItem(
                userId,
                productId);

            return Ok("Item added to wishlist");
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var userId = GetUserId();

            await _wishlistService.RemoveItem(
                userId,
                id);

            return Ok("Item removed from wishlist");
        }
    }
}