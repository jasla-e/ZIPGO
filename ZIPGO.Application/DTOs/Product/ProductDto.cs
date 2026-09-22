using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Rating { get; set; }

        public int Stock { get; set; }

        public string Image { get; set; }

        public bool Offer { get; set; }

        public int MainCategoryId { get; set; }

        public int SubCategoryId { get; set; }
    }
}