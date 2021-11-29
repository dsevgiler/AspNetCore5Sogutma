using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Abstract;
using Web.Models.User;

namespace Web.Utilities
{
    public class WebContext : IWebContext
    {
        private AccessToken _accessToken;
        private AppUser _appUser;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICookieAuthenticationService _authenticationService;

        public WebContext(IHttpContextAccessor httpContextAccessor, ICookieAuthenticationService authnticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authnticationService;
        }

        public AccessToken JwtAccessToken
        {
            get
            {
                if (_accessToken != null)
                    return _accessToken;

                var accessToken = _authenticationService.GetAppToken();
                _accessToken = accessToken;

                return _accessToken;
            }
            set
            {
                this._accessToken = value;
            }
        }

        public AppUser CurrentAppUser
        {
            get
            {
                if (_appUser != null)
                    return _appUser;

                var appUser = _authenticationService.GetAppUser();
                _appUser = appUser;

                return _appUser;
            }
            set
            {
                this._appUser = value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return _authenticationService.GetAppUserRoles().Any(F => F.Name == "Admin");
            }
        }

        public bool IsEditor
        {
            get
            {
                return _authenticationService.GetAppUserRoles().Any(F => F.Name == "Editor");
            }
        }

        public bool IsStandartUser
        {
            get
            {
                return _authenticationService.GetAppUserRoles().Any(F => F.Name == "Standart");
            }
        }

    }
}
