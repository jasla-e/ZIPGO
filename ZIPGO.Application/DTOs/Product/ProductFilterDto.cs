using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Product
{
    public class ProductFilterDto
    {
        public string? Search { get; set; }

        public int? MainCategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public string? PriceRange { get; set; }

        public string? Sort { get; set; }
    }
}