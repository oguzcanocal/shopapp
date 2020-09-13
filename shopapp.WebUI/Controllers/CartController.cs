using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.Common.Dto;
using shopapp.Common.Extensions;
using shopapp.Domain.Entities;
using shopapp.Web.Identity;
using shopapp.WebUI.Controllers;
using shoppapp.Services.Abstract;

namespace shopapp.Web.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private ICartService _cartService;
        private IOrderService _orderService;
        private UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _orderService = orderService;
        }


        public IActionResult Index()
        {
            string userid = _userManager.GetUserId(User);
            var cart = _cartService.GetCartByUserId(userid);
            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()//her bir select işleminde yeni bir CartItemModel oluşturuyorum.
                {
                    CartItemId = i.Id,
                    Name = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity

                }).ToList()

            });
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            string userid = _userManager.GetUserId(User);
            _cartService.AddToCart(userid, productId, quantity);
            _cartService.Save();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            _cartService.DeleteFromCart(_userManager.GetUserId(User), productId);
            _cartService.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var orderModel = new OrderModel();

            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()//her bir select işleminde yeni bir CartItemModel oluşturuyorum.
                {
                    CartItemId = i.Id,
                    Name = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity

                }).ToList()
            };
            return View(orderModel);


        }
        [HttpPost]
        public IActionResult Checkout(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cart = _cartService.GetCartByUserId(userId);
                model.CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        Name = i.Product.Name,
                        Price = (decimal)i.Product.Price,
                        ImageUrl = i.Product.ImageUrl,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity
                    }).ToList()
                };

                //ödeme
                var payment = PaymentProcess(model);

                if (payment.Status == "success")
                {
                    //db'ye order kaydetme
                    SaveOrder(model, payment, userId);
                    //cart'ı temizleme
                    ClearCart(cart.Id.ToString());
                    return View("Success");
                }
            }

            return View();
        }

        private void ClearCart(string cartId)
        {
            _cartService.ClearCart(cartId);
            _cartService.Save();
        }

        private void SaveOrder(OrderModel model, Payment payment, string userId)
        {
            var order = new Order();
            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.OrderDate = new DateTime();
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Email = model.Email;
            order.Phone = model.Phone;
            order.Address = model.Address;
            order.UserId = userId;
            foreach (var item in model.CartModel.CartItems)
            {
                var orderitem = new OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                };
                order.OrderItems.Add(orderitem);
            }
            _orderService.Create(order);
            _orderService.Save();
        }

        private Payment PaymentProcess(OrderModel model)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-78ZL1joCSBbbV3lxeOVnoC5JrEQvIdWw";
            options.SecretKey = "sandbox-cWwLdKUxZFxE13yMbWkB3odSoSOtr8aH";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = model.CartModel.TotalPrice().ToString().Split(",")[0]; ;
            request.PaidPrice = model.CartModel.TotalPrice().ToString().Split(",")[0]; ;
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = model.CartModel.CartId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.CVV;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            //paymentCard.CardHolderName = "John Doe";
            //paymentCard.CardNumber = "5528790000000008";
            //paymentCard.ExpireMonth = "12";
            //paymentCard.ExpireYear = "2030";
            //paymentCard.Cvc = "123";

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;

            foreach (var item in model.CartModel.CartItems)
            {
                var TotalPrice = item.Price * item.Quantity;

                basketItem = new BasketItem();
                basketItem.Id = item.ProductId.ToString();
                basketItem.Name = item.Name;
                basketItem.Category1 = "Phone";
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItem.Price = TotalPrice.ToString().Split(",")[0];


                basketItems.Add(basketItem);
            }

            request.BasketItems = basketItems;

            return Payment.Create(request, options);

        }

        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders(_userManager.GetUserId(User));
            if (orders.Count <= 0)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Ürünler",
                    Message = "Satın aldığınız ürün bulunmamaktadır.",
                    Css = "warning"
                });
            }
            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;

            foreach (var order in orders)
            {
                orderModel = new OrderListModel();
                orderModel.OrderId = order.OrderId;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.OrderNote = order.OrderNote;
                orderModel.Phone = order.Phone;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Email = order.Email;
                orderModel.Address = order.Address;
                orderModel.City = order.City;

                orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.Id,
                    Name = i.Product.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ImageUrl = i.Product.ImageUrl
                }).ToList();

                orderListModel.Add(orderModel);

            }

            return View(orderListModel);
        }
    }
}
