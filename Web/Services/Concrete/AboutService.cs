using Entities.Concrete;
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
    public class AboutService : IAboutService
    {
        private IHttpHelper _httpHelper;

        public AboutService(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public IDataResult<About> GetById(int id)
        {
            var request = _httpHelper.GetRequest("/Abouts/getbyid/" + id);
            var result = request.ToDataResult<About>();

            return result;
        }

        public IDataResult<About> Update(About about)
        {
            var request = _httpHelper.PostRequest("/Abouts/Update", about);
            var result = request.ToDataResult<About>();

            return result;
        }
    }
}
