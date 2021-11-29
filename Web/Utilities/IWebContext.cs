using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.User;

namespace Web.Utilities
{
    public interface IWebContext
    {
        AccessToken JwtAccessToken { get; }

        AppUser CurrentAppUser { get; set; }

        bool IsAdmin { get; }

        bool IsEditor { get; }

        bool IsStandartUser { get; } 

    }
}
