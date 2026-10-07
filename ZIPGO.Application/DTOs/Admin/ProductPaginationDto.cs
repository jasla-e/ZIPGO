using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Product
{
    public class ProductPaginationDto
    {
        public string? Search { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 4;
    }
}