using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class SliderManager : ISliderService
    {
        private readonly ISliderDal _sliderDal;

        public SliderManager(ISliderDal sliderDal)
        {
            _sliderDal = sliderDal;
        }

        public IDataResult<Slider> GetById(int sliderId)
        {
            var result = _sliderDal.Get(p => p.Id == sliderId);
            return result.ReturnDataResult();
        }

        public IDataResult<List<Slider>> GetList()
        {
            var result = _sliderDal.GetList();
            return result.ReturnDataResult();
        }

        public IResult Add(Slider slider)
        {
            _sliderDal.Add(slider);
            return new SuccessResult(Messages.SliderAdded);
        }

        public IResult Update(Slider slider)
        {
            _sliderDal.Update(slider);
            return new SuccessResult(Messages.SliderUpdated);
        }

        public IResult Delete(Slider slider)
        {
            _sliderDal.Delete(slider);
            return new SuccessResult(Messages.SliderDeleted);
        }

    }
}
