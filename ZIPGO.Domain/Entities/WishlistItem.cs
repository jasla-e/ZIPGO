using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
    public class WishlistItem
    {
        public int Id { get; set; }

        public int WishlistId { get; set; }

        public Wishlist Wishlist { get; set; } = null!;

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }
}