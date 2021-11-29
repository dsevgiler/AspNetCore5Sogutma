using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }

        // Refresh Token nedeni token ın süresi bitti kullanıcıyı login ekranına göndermek yerine yeni bir token üretmek için..   
    }
}
