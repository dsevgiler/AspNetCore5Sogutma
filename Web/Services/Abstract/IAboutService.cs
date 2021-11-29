using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface IAboutService
    {
        IDataResult<About> Update(About about);
        IDataResult<About> GetById(int id);
    }
}
