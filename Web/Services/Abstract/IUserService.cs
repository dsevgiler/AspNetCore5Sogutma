using Entities.Dtos;
using Web.Models.User;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface IUserService
    {
        IDataResult<AccessToken> LoginToApi(UserLoginDto model);
        AppUser GetUserByEmail(string email = "");
        AppUser GetUserByUsername(string username = "");
    }
}
