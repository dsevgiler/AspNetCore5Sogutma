using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Web.Models.User;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Web.Utilities;
using Web.Services.Abstract;
using Web.Utilities.Results;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ICookieAuthenticationService _authenticationService;
        private readonly INotification _notification;
        private readonly IUserService _applicationUserService;

        public UserController(ICookieAuthenticationService authenticationService, IUserService applicationUserService, INotification notification)
        {
            _authenticationService = authenticationService;
            _applicationUserService = applicationUserService;
            _notification = notification;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            _authenticationService.SignOut();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginDto);
            }

            IDataResult<AccessToken> dataResult = _applicationUserService.LoginToApi(userLoginDto);
            if (!dataResult.Success || dataResult.Data == null)
            {
                _notification.Error("Giriş denemesi başarısız lütfen bilgilerinizi kontrol ederek tekrar deneyiniz. Hata Ayrıntısı : \n " + dataResult.Message);
                return View(userLoginDto);
            }

            _authenticationService.SignIn(dataResult.Data, false);

            return RedirectToAction("Index","Home", new { area = "Admin" });
        }

        public async Task<IActionResult> LogOut()
        {
            _authenticationService.SignOut();
            return RedirectToAction("Login");
        }
    }
}
