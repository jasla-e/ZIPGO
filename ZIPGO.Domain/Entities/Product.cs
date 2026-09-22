using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Rating { get; set; }

        public int Stock { get; set; }

        public string Image { get; set; }

        public bool Offer {  get; set; }

        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; } = null!;

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; } = null!;

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
