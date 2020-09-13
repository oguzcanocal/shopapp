using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.Common.Dto;
using shopapp.Domain.Entities;
using shoppapp.Services.Abstract;

namespace shopapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("/cart/getcartbyuserid")]
        public Cart GetCartbyUserId(string userid)
        {
            Cart cart = _cartService.GetCartByUserId(userid);
            return cart;
        }
        //[HttpPost("/cart/addtocart")]
        //public void AddtoCart(int productId,int quantity)
        //{
        //    _cartService.AddToCart(_userManager.GetUserId(User), productId, quantity);
        //}
    }
}
