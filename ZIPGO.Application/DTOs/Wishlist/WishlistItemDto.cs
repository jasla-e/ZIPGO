using ZIPGO.Application.DTOs.Product;

namespace ZIPGO.Application.DTOs.Wishlist
{
    public class WishlistItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; } = null!;
    }
}