using Entities.Concrete;
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
    public class SliderController : BaseController
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public ActionResult Index()
        {
            var result = _sliderService.GetList();
            return View(result.Data);
        }

        public ActionResult Details(int id)
        {
            var result = _sliderService.GetById(id);
            return View(result.Data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Slider slider, IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    var fileName = string.Format($"{Guid.NewGuid()}{extention}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\sliders", fileName);
                    slider.ImageUrl = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _sliderService.Add(slider);
                }
                catch (Exception e)
                {

                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _sliderService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Slider slider, IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    var fileName = string.Format($"{Guid.NewGuid()}{extention}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\sliders", fileName);
                    slider.ImageUrl = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _sliderService.Update(slider);
                }
                catch (Exception e)
                {

                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _sliderService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Slider slider)
        {
            _sliderService.Delete(slider);
            return RedirectToAction(nameof(Index));
        }
    }

}
