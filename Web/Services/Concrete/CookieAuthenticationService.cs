using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Web.Services.Abstract;
using Web.Models.User;


namespace Web.Services.Concrete
{
    public class CookieAuthenticationService : ICookieAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _applicationUserService;

        private AppUser _appUser;
        private List<AppUserRole> _appUserRoles;
        private AccessToken _accessToken;

        public CookieAuthenticationService(IHttpContextAccessor httpContextAccessor, IUserService applicationUserService)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationUserService = applicationUserService;
        }

        public virtual AccessToken GetAppToken()
        {
            if (_accessToken != null)
                return _accessToken;

            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(Schema.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            var token = new AccessToken();

            var tokenClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == "Token"
                && claim.Issuer.Equals(Schema.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));

            var tokenExpirationClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == "TokenExpiration"
                && claim.Issuer.Equals(Schema.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(tokenClaim.Value))
                token.Token = tokenClaim.Value;

            if (!string.IsNullOrEmpty(tokenExpirationClaim.Value))
                token.Expiration = Convert.ToDateTime(tokenExpirationClaim.Value);

            _accessToken = token;

            return _accessToken;
        }

        public virtual AppUser GetAppUser()
        {
            if (_appUser != null)
                return _appUser;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(Schema.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            AppUser user = null;

            ////try to get user by email
            var emailClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == JwtRegisteredClaimNames.Email
                && claim.Issuer.Equals(Schema.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));

            if (emailClaim != null && !string.IsNullOrEmpty(emailClaim.Value))
                user = _applicationUserService.GetUserByEmail(emailClaim.Value);

            ////try to get user by username
            var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == JwtRegisteredClaimNames.UniqueName
                && claim.Issuer.Equals(Schema.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));

            if (usernameClaim != null && !string.IsNullOrEmpty(usernameClaim.Value))
                user = _applicationUserService.GetUserByUsername(usernameClaim.Value);

            //cache authenticated user
            _appUser = user;

            return _appUser;
        }

        public virtual List<AppUserRole> GetAppUserRoles()
        {
            if (_appUserRoles != null)
                return _appUserRoles;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(Schema.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            var roles = authenticateResult.Principal.FindAll(claim => claim.Type == ClaimTypes.Role
              && claim.Issuer.Equals(Schema.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));

            _appUserRoles = roles.Select(F => new AppUserRole { Name = F.Value }).ToList();

            return _appUserRoles;
        }

        public virtual async void SignIn(AccessToken accessToken, bool isPersistent)
        {
            if (accessToken == null)
                throw new ArgumentNullException(nameof(accessToken));

            var claims = new List<Claim>();

            if (accessToken != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(accessToken.Token);

                var nameIdentifierClaim = token.Claims.FirstOrDefault(F => F.Type == ClaimTypes.NameIdentifier);
                if (nameIdentifierClaim != null)
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifierClaim.Value, ClaimValueTypes.String, Schema.ClaimsIssuer));

                var nameClaim = token.Claims.FirstOrDefault(F => F.Type == ClaimTypes.Name);
                if (nameClaim != null)
                    claims.Add(new Claim(ClaimTypes.Name, nameClaim.Value, ClaimValueTypes.String, Schema.ClaimsIssuer));

                var emailClaim = token.Claims.FirstOrDefault(F => F.Type == JwtRegisteredClaimNames.Email);
                if (emailClaim != null)
                    claims.Add(new Claim(JwtRegisteredClaimNames.Email, emailClaim.Value, ClaimValueTypes.String, Schema.ClaimsIssuer));

                var usernameClaim = token.Claims.FirstOrDefault(f => f.Type == JwtRegisteredClaimNames.UniqueName);
                if (usernameClaim != null)
                    claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, usernameClaim.Value, ClaimValueTypes.String, Schema.ClaimsIssuer));

                if (!string.IsNullOrEmpty(accessToken.Token))
                    claims.Add(new Claim("Token", accessToken.Token, ClaimValueTypes.String, Schema.ClaimsIssuer));

                if (accessToken.Expiration.HasValue)
                {
                    string expirationDate = accessToken.Expiration.Value.ToString();
                    claims.Add(new Claim("TokenExpiration", expirationDate, ClaimValueTypes.String, Schema.ClaimsIssuer));
                }

                if (token.Claims.Count(f => f.Type == ClaimTypes.Role) > 0)
                {
                    foreach (var role in token.Claims.Where(f => f.Type == ClaimTypes.Role))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Value, ClaimValueTypes.String, Schema.ClaimsIssuer));
                    }
                }
            }

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, Schema.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                ExpiresUtc = accessToken.Expiration,
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(Schema.AuthenticationScheme, userPrincipal, authenticationProperties);

            //cache token
            _accessToken = accessToken;
        }

        public virtual async void SignOut()
        {
           await _httpContextAccessor.HttpContext.SignOutAsync(Schema.AuthenticationScheme);
        }
    }
}
