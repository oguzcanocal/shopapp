﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shopapp.Common.Dto;
using shopapp.Domain.Entities;
using shoppapp.Services.Abstract;

namespace shopapp.WebUI.Controllers
{
    public class ShopController : BaseController
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(new ProductDetailsModel()
            {
                Product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()
            });
        }

        //public IActionResult List(string category, int page = 1)
        //{
        //    const int pageSize = 3;
        //    return View(new ProductListModel()
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
