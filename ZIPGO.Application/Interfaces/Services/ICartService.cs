using ZIPGO.Application.DTOs.Cart;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto?> GetCart(int userId);

        Task AddItem(int userId, int productId, int quantity);

        Task UpdateItem(int userId, int cartItemId, int quantity);

        Task RemoveItem(int userId, int cartItemId);

        Task ClearCart(int userId);
    }
}