using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services.Concrete
{
    public class Schema
    {
        public static string AuthenticationScheme => "Authentication";
        public static string ClaimsIssuer => "Ucsan";
        public static string AuthenticationCookie => ".Authentication";
        public static PathString LoginPath => new PathString("/user/login");
        public static PathString LogoutPath => new PathString("/user/logout");
        public static PathString AccessDeniedPath => new PathString("/page-not-found");

        public static string Prefix => ".Ucsan";
        public static string SessionCookie => ".Session";
        public static string TempDataCookie => ".TempData";
        public static string AntiforgeryCookie => ".Antiforgery";
    }
}
