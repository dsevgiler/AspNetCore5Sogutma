using Core.Entites.Concrete;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        private IHttpContextAccessor _httpContextAccessor;

        public JwtHelper(IConfiguration configuration)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            var refreshToken = CreateRefreshToken();

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                RefreshToken = refreshToken
            };
        }

        //Refresh Token üretecek metot.
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }


        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c=>c.Name).ToArray());

            return claims;
        }


        private List<string> GetClaims(string claimType)
        {
            var _claims = _httpContextAccessor.HttpContext.User.Claims
                .Where(F => F.Type == claimType)
                .Select(F => F.Value)
                .ToList();
            if (_claims == null)
            {
                _claims = new List<string>();
            }
            return _claims;
        }

        public string GetEmail()
        {
            return GetClaims(ClaimTypes.Email).FirstOrDefault();
        }
        public string GetName()
        {
            return GetClaims(ClaimTypes.Name).FirstOrDefault();
        }

        public string GetNameIdentifier()
        {
            return GetClaims(ClaimTypes.NameIdentifier).FirstOrDefault();
        }

        public List<string> GetRoles()
        {
            return GetClaims(ClaimTypes.Role);
        }

    }
}
