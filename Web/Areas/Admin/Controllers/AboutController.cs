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
    public class AboutController : BaseController
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public ActionResult Edit(int id)
        {
            var result = _aboutService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(About about, IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    var fileName = string.Format($"{Guid.NewGuid()}{extention}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\about", fileName);
                    about.ImageUrl = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _aboutService.Update(about);
                }
                catch (Exception e)
                {

                }
            }

            return RedirectToAction("Edit");
        }

    }
}
