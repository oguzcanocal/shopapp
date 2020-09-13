using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopapp.Common.Dto;
using shoppapp.Services.Abstract;

namespace shopapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("/home/index")]
        public ProductListModel Index()
        {
            return (new ProductListModel()
            {
                Products = _productService.GetAll()

            });
        }
    }
}
