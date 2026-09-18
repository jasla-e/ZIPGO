using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetByUserId(int userId);
        Task Add(Cart cart);
        Task ClearCart(int userId);
    }
}