using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.SubCategory
{
    public class SubCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MainCategoryId { get; set; }
    }
}