using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ZIPGO.Application.DTOs.Product
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public decimal Rating { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; }

        [Required]
        public string Image { get; set; }

        public bool Offer { get; set; }

        public int MainCategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "SubCategoryId must be greater than 0")]
        public int SubCategoryId { get; set; }



    }
}