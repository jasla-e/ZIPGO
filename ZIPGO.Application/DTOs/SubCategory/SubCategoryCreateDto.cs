using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.SubCategory
{
    public class SubCategoryCreateDto
    {
        public string Name { get; set; }

        public int MainCategoryId { get; set; }
    }
}