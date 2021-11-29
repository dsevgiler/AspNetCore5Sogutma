using Business.Abstract;
using Core.Entites.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u=>u.Email == email);
        }

        public IDataResult<User> GetDataByMail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);
            return new SuccessDataResult<User>(user);
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }

        public User Get(string filter)
        {
            return _userDal.Get(x=>x.RefreshToken == filter);
        }
    }
}
