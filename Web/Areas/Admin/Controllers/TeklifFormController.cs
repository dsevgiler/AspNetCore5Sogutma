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
    public class TeklifFormController : BaseController
    {
        private readonly ITeklifFormService _teklifFormService;

        public TeklifFormController(ITeklifFormService teklifFormService)
        {
            _teklifFormService = teklifFormService;
        }

        public IActionResult Index()
        {
            var result = _teklifFormService.GetList();
            return View(result.Data);
        }

        public ActionResult Details(int id)
        {
            var result = _teklifFormService.GetById(id);
            return View(result.Data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeklifForm teklifForm)
        {
            _teklifFormService.Add(teklifForm);
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _teklifFormService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeklifForm teklifForm)
        {
            _teklifFormService.Update(teklifForm);
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _teklifFormService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TeklifForm teklifForm)
        {
            _teklifFormService.Delete(teklifForm);
            return RedirectToAction(nameof(Index));
        }
    }
}
