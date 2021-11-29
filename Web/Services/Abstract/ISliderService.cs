using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface ISliderService
    {
        IDataResult<List<Slider>> GetList();
        IDataResult<Slider> GetById(int id);
        IDataResult<Slider> Add(Slider slider);
        IDataResult<Slider> Update(Slider slider);
        IDataResult<Slider> Delete(Slider slider);
    }
}
