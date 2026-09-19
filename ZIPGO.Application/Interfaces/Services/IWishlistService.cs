using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs.Wishlist;

using ZIPGO.Application.DTOs.Wishlist;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IWishlistService
    {
        Task<WishlistDto?> GetWishlist(int userId);

        Task AddItem(int userId, int productId);

        Task RemoveItem(int userId, int wishlistItemId);
    }
}