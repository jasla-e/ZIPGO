using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Product
{
    public class ProductPagedResultDto
    {
        public List<ProductDto> Products { get; set; } = new();

        public int TotalCount { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }
    }
}