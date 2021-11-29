using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.User;

namespace Web.Services.Abstract
{
    public interface ICookieAuthenticationService
    {
        AccessToken GetAppToken();
        AppUser GetAppUser();
        List<AppUserRole> GetAppUserRoles();
        void SignIn(AccessToken accessToken, bool isPersistent);
        void SignOut();
    }
}
