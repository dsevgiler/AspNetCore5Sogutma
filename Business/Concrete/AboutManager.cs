using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AboutManager : IAboutService
    {
        private IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public IDataResult<About> GetById(int id)
        {
            var result = _aboutDal.Get(p => p.Id == id);
            return result.ReturnDataResult();
        }

        public IResult Update(About about)
        {
            _aboutDal.Update(about);
            return new SuccessResult(Messages.AboutUpdated);
        }
    }
}
