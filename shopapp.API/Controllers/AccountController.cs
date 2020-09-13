using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoppapp.Services.Abstract;

namespace shopapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ICartService _cartService;
        public AccountController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("/cart/initial")]
        public void InitializeCart(string userid)
        {
            _cartService.InitializeCart(userid);
        }
    }
}
