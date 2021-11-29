using Core.Entites.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        void Update(User user);
        User GetByMail(string email);
        User Get(string filter);
        IDataResult<User> GetDataByMail(string email);
    }
}
