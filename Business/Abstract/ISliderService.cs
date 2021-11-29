using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ISliderService
    {
        IDataResult<Slider> GetById(int sliderId);
        IDataResult<List<Slider>> GetList();
        IResult Add(Slider slider);
        IResult Delete(Slider slider);
        IResult Update(Slider slider);
    }
}
