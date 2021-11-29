using Entities.Dtos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Abstract;
using Web.Models.User;
using Web.Utilities.Extentions;
using Web.Utilities.HttpHelpers;
using Web.Utilities.Results;

namespace Web.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IHttpHelper _httpHelper;
        private readonly INotification _notification;

        public UserService(IHttpHelper httpHelper, INotification notification)
        {
            _httpHelper = httpHelper;
            _notification = notification;
        }

        public AppUser GetUserByEmail(string email = "")
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            var request = _httpHelper.PostRequest("/user/getbyemail", new { email = email });
            var result = request.ToDataResult<AppUser>();
            if (result == null)
            {
                return new AppUser();
            }
            return result.Data;
        }

        public AppUser GetUserByUsername(string username = "")
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");

            var parameters = new JsonObject { { "emailOrUsername", username } };
            var request = _httpHelper.PostRequest("/user/getbyusername", parameters);
            var result = request.ToDataResult<AppUser>();
            if (result == null)
            {
                throw new ArgumentNullException("ApplicationUser");
            }
            return result.Data;
        }

        public IDataResult<AccessToken> LoginToApi(UserLoginDto model)
        {
            var request = _httpHelper.PostRequest("/auth/login", model, true);
            var result = request.ToDataResult<AccessToken>(_notification);
            return result;
        }
    }
}
