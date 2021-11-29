using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IWebContext _webContext;

        public HomeController(IWebContext webContext)
        {
            _webContext = webContext;
        }

        public IActionResult Index()
        {
            ViewBag.ConnectedUser = _webContext.CurrentAppUser.GetFullName(); 
            return View();
        }
    }
}
