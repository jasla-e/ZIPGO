using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Cart
{
    public class CartDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public List<CartItemDto> CartItems { get; set; }
            = new List<CartItemDto>();
    }
}