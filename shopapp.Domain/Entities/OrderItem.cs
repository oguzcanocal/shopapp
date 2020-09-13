using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }//Order ekranına geçtiğimde cart ekranında fiyat güncellense bile order ekranında fiyatı aynı tutuyorum.
        public int Quantity { get; set; }
    }
}
