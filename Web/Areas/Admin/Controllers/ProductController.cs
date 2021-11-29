using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Abstract;
using Web.Utilities;

namespace Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var result = _productService.GetList();
            return View(result.Data);
        }

        public ActionResult Details(int id)
        {
            var result = _productService.GetById(id);
            return View(result.Data);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryList = _categoryService.GetSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDto product, IFormFile file)
        {
            if (file !=null)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    var fileName = string.Format($"{Guid.NewGuid()}{extention}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\products", fileName);
                    product.ImageUrl = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var result = _productService.Add(product);

                    ViewBag.CategoryList = _categoryService.GetSelectList();
                }
                catch (Exception e)
                {

                }
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            ViewBag.CategoryList = _categoryService.GetSelectList();

            var result = _productService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product, IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    var fileName = string.Format($"{Guid.NewGuid()}{extention}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\products", fileName);
                    product.ImageUrl = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var result = _productService.Update(product);

                    ViewBag.CategoryList = _categoryService.GetSelectList();
                }
                catch (Exception e)
                {

                }
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            var result = _productService.GetById2(id);
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product product)
        {
            _productService.Delete(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
