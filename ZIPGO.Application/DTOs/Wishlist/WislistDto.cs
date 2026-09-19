using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Wishlist
{
    public class WishlistDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public List<WishlistItemDto> WishlistItems { get; set; }
            = new List<WishlistItemDto>();
    }
}