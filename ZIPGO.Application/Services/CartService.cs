using ZIPGO.Application.DTOs.Cart;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task AddItem(int userId, int productId, int quantity)
        {
            var cart = await _cartRepository.GetByUserId(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };

                await _cartRepository.Add(cart);
            }

            var existingItem =
                await _cartItemRepository.GetByCartAndProduct(
                    cart.Id,
                    productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;

                await _cartItemRepository.Update(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };

                await _cartItemRepository.Add(cartItem);
            }
        }

        public async Task<CartDto?> GetCart(int userId)
        {
            var cart = await _cartRepository.GetByUserId(userId);

            if (cart == null)
                return null;

            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public async Task UpdateItem(
            int userId,
            int cartItemId,
            int quantity)
        {
            var cart = await _cartRepository.GetByUserId(userId);

            if (cart == null)
                return;

            var item = await _cartItemRepository.GetById(cartItemId);

            if (item == null || item.CartId != cart.Id)
                return;

            item.Quantity = quantity;

            await _cartItemRepository.Update(item);
        }

        public async Task RemoveItem(
            int userId,
            int cartItemId)
        {
            var cart = await _cartRepository.GetByUserId(userId);

            if (cart == null)
                return;

            var item = await _cartItemRepository.GetById(cartItemId);

            if (item == null || item.CartId != cart.Id)
                return;

            await _cartItemRepository.Remove(cartItemId);
        }

        public async Task ClearCart(int userId)
        {
            await _cartRepository.ClearCart(userId);
        }
    }
}