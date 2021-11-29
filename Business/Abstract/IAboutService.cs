using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAboutService
    {
        IResult Update(About about);
        IDataResult<About> GetById(int id);
    }
}
