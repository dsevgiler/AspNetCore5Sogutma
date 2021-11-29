using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Abstract;
using Web.Utilities.Extentions;
using Web.Utilities.HttpHelpers;
using Web.Utilities.Results;

namespace Web.Services.Concrete
{
    public class SliderService : ISliderService
    {
        private readonly IHttpHelper _httpHelper;

        public SliderService(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public IDataResult<Slider> Add(Slider slider)
        {
            var request = _httpHelper.PostRequest("/Sliders/Add", slider);
            var result = request.ToDataResult<Slider>();

            return result;
        }

        public IDataResult<Slider> Delete(Slider slider)
        {
            var request = _httpHelper.PostRequest("/Sliders/Delete", slider);
            var result = request.ToDataResult<Slider>();

            return result;
        }

        public IDataResult<List<Slider>> GetList()
        {
            var request = _httpHelper.GetRequest("/Sliders/getall");
            var result = request.ToDataResult<List<Slider>>();

            return result;
        }

        public IDataResult<Slider> Update(Slider slider)
        {
            var request = _httpHelper.PostRequest("/Sliders/Update", slider);
            var result = request.ToDataResult<Slider>();

            return result;
        }

        public IDataResult<Slider> GetById(int sliderId)
        {
            var request = _httpHelper.GetRequest("/Sliders/getbyid/" + sliderId);
            var result = request.ToDataResult<Slider>();

            return result;
        }
    }
}
