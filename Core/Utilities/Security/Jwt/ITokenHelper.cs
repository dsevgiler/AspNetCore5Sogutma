using System;
using System.Collections.Generic;
using System.Text;
using Core.Entites.Concrete;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper 
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims); // kullanıcı bilgisi ve rolleri jwt de olsun

        string GetEmail();
        string GetName();
        string GetNameIdentifier();
        List<string> GetRoles();
    }
}
