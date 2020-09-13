using shopapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace shopapp.Common.Dto
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Ürün ismi min. 5 karakter max. 60 karakter olmalıdır.")]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Fiyat belirtiniz")]
        [Range(1, 100000)]
        public decimal? Price { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Ürün açıklaması min. 10 karakter max. 60 karakter olmalıdır.")]
        public string Description { get; set; }

        public List<Category> SelectedCategories { get; set; }
    }
}
