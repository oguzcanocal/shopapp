using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopapp.Common.Dto;
using shopapp.Domain.Entities;
using shoppapp.Services.Abstract;

namespace shopapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        //[HttpGet("/shops/details")]
        //public Product Details(int? id)
        //{
        //    Product product = _productService.GetProductDetails((int)id);

        //    return product;
        //}


        //[HttpGet("/shop/list")]
        //public ProductListModel List(string category, int page = 1)
        //{
        //    const int pageSize = 3;
        //    return (new ProductListModel()
        //    {
        //        PageInfo = new PageInfo()
        //        {
        //            TotalItems = _productService.GetCountByCategory(category),
        //            CurrentPage = page,
        //            ItemsPerPage = pageSize,
        //            CurrentCategory = category
        //        },
        //        Products = _productService.GetProductsByCategory(category, page, pageSize)
        //    });
        //}
    }
}
