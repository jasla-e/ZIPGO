using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs.Wishlist;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IWishlistItemRepository _wishlistItemRepository;

        public WishlistService(
            IWishlistRepository wishlistRepository,
            IWishlistItemRepository wishlistItemRepository)
        {
            _wishlistRepository = wishlistRepository;
            _wishlistItemRepository = wishlistItemRepository;
        }

        public async Task<WishlistDto?> GetWishlist(int userId)
        {
            var wishlist = await _wishlistRepository.GetByUserId(userId);

            if (wishlist == null)
                return null;

            return new WishlistDto
            {
                Id = wishlist.Id,
                UserId = wishlist.UserId,

                WishlistItems = wishlist.WishlistItems
                    .Select(item => new WishlistItemDto
                    {
                        Id = item.Id,
                        ProductId = item.ProductId
                    })
                    .ToList()
            };
        }

        public async Task AddItem(int userId, int productId)
        {
            var wishlist = await _wishlistRepository.GetByUserId(userId);

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId
                };

                await _wishlistRepository.Add(wishlist);
            }

            var existingItem =
                await _wishlistItemRepository.GetByWishlistAndProduct(
                    wishlist.Id,
                    productId);

            if (existingItem != null)
                return;

            var wishlistItem = new WishlistItem
            {
                WishlistId = wishlist.Id,
                ProductId = productId
            };

            await _wishlistItemRepository.Add(wishlistItem);
        }

        public async Task RemoveItem(int userId, int wishlistItemId)
        {
            var wishlist = await _wishlistRepository.GetByUserId(userId);

            if (wishlist == null)
                return;

            var item = await _wishlistItemRepository.GetById(wishlistItemId);

            if (item == null || item.WishlistId != wishlist.Id)
                return;

            await _wishlistItemRepository.Remove(wishlistItemId);
        }
    }
}