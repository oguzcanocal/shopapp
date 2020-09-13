using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Common.Dto
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int CartId { get; set; }
        public CartDto Cart { get; set; }
        public int Quantity { get; set; }
    }
}
