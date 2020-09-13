using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Common.Dto
{
    public class CartDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
