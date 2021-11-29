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
    public class ServiceController : BaseController
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public ActionResult Index()
        {
            var result = _serviceService.GetList();
            return View(result.Data);
        }

        public ActionResult Details(int id)
        {
            var result = _serviceService.GetById(id);
            return View(result.Data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Service service, IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    var fileName = string.Format($"{Guid.NewGuid()}{extention}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\services", fileName);
                    service.ImageUrl = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _serviceService.Add(service);
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
            var result = _serviceService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Service service, IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    var fileName = string.Format($"{Guid.NewGuid()}{extention}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\services", fileName);
                    service.ImageUrl = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _serviceService.Update(service);
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
            var result = _serviceService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Service service)
        {
            _serviceService.Delete(service);
            return RedirectToAction(nameof(Index));
        }
    }

}
