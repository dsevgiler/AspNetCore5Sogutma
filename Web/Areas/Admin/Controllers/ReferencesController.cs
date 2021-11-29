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
    public class ReferencesController : BaseController
    {
        private readonly IReferenceService _referencesService;

        public ReferencesController(IReferenceService referencesService)
        {
            _referencesService = referencesService;
        }

        public IActionResult Index()
        {
            var result = _referencesService.GetList();
            return View(result.Data);
        }

        public ActionResult Details(int id)
        {
            var result = _referencesService.GetById(id);
            return View(result.Data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Reference reference, IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    var fileName = string.Format($"{Guid.NewGuid()}{extention}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\references", fileName);
                    reference.ImageUrl = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _referencesService.Add(reference);
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
            var result = _referencesService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Reference reference, IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    var fileName = string.Format($"{Guid.NewGuid()}{extention}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\references", fileName);
                    reference.ImageUrl = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _referencesService.Update(reference);
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
            var result = _referencesService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Reference reference)
        {
            _referencesService.Delete(reference);
            return RedirectToAction(nameof(Index));
        }
    }
}
