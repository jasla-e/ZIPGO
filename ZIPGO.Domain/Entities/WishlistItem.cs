using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
    public class WishlistItem
    {
        public int Id { get; set; }

        public int WishlistId { get; set; }

        public int ProductId { get; set; }

    }
}
