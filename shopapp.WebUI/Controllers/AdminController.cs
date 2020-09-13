using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopapp.Common.Dto;
using shopapp.Domain.Entities;
using shoppapp.Services.Abstract;

namespace shopapp.Web.Controllers
{

    [Authorize(Roles = "admin")]//Bu alana giriş yapılabilmesi için kullanıcının login işlemi yapmış olması gerekiyor.
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult ListProduct()
        {
            return View(new ProductListModel()
            {

                Products = _productService.GetAll()
            });
        }

        public IActionResult CreateProduct()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            if (ModelState.IsValid == true)
            {
                var entity = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };

                _productService.Create(entity);
                _productService.Save();
                return RedirectToAction("ListProduct");
            }

            return View(model);
        }

        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _productService.GetByIdWithCategories((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                SelectedCategories = entity.ProductCategories.Select(x => x.Category).ToList()
            };

            ViewBag.Categories = _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                var entity = _productService.GetByIdWithCategories(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Price = model.Price;

                if (file != null)
                {
                    entity.ImageUrl = file.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _productService.Update(entity, categoryIds);
                _productService.Save();

                return RedirectToAction("ListProduct");
            }

            ViewBag.Categories = _categoryService.GetAll();

            return View(model);

        }
        [HttpPost]
        public IActionResult DeleteProduct(int productid)
        {
            var entity = _productService.GetById(productid);

            if (entity != null)
            {
                _productService.Delete(entity);
                _productService.Save();
            }
            return RedirectToAction("ListProduct");
        }

        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {

                Categories = _categoryService.GetAll()
            });
        }

        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name
            };

            _categoryService.Create(entity);
            _categoryService.Save();
            
            return RedirectToAction("CategoryList");
        }

        public IActionResult EditCategory(int id)
        {
            var entity = _categoryService.GetByIdWithProducts(id);
            return View(new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()

            }); ;
        }


        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            _categoryService.Update(entity);
            _categoryService.Save();
            return RedirectToAction("CategoryList");
        }


        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);

            if (entity != null)
            {
                _categoryService.Delete(entity);
            }
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int productId, int categoryId)
        {
            _categoryService.DeleteFromCategoty(categoryId, productId);
            _categoryService.Save();
            return Redirect("/admin/editcategory/" + categoryId);
        }
    }
}

