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
    public class ContactFormController : BaseController
    {
        private readonly IContactFormService _contactFormService;

        public ContactFormController(IContactFormService contactFormService)
        {
            _contactFormService = contactFormService;
        }

        public IActionResult Index()
        {
            var result = _contactFormService.GetList();
            return View(result.Data);
        }

        public ActionResult Details(int id)
        {
            var result = _contactFormService.GetById(id);
            return View(result.Data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactForm mailForm)
        {
            _contactFormService.Add(mailForm);
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _contactFormService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactForm mailForm)
        {
            _contactFormService.Update(mailForm);
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _contactFormService.GetById(id);
            return View(result.Data);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ContactForm reference)
        {
            _contactFormService.Delete(reference);
            return RedirectToAction(nameof(Index));
        }
    }
}
