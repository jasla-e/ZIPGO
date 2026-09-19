using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface IWishlistItemRepository
    {
        Task<WishlistItem?> GetById(int id);

        Task<WishlistItem?> GetByWishlistAndProduct( int wishlistId, int productId);

        Task Add(WishlistItem wishlistItem);

        Task Remove(int id);



    }
}
