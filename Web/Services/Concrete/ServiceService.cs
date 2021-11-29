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
    public class ServiceService : IServiceService
    {
        private readonly IHttpHelper _httpHelper;

        public ServiceService(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public IDataResult<Service> Add(Service service)
        {
            var request = _httpHelper.PostRequest("/Services/Add", service);
            var result = request.ToDataResult<Service>();

            return result;
        }

        public IDataResult<Service> Delete(Service slider)
        {
            var request = _httpHelper.PostRequest("/Services/Delete", slider);
            var result = request.ToDataResult<Service>();

            return result;
        }

        public IDataResult<List<Service>> GetList()
        {
            var request = _httpHelper.GetRequest("/Services/getall");
            var result = request.ToDataResult<List<Service>>();

            return result;
        }

        public IDataResult<Service> Update(Service slider)
        {
            var request = _httpHelper.PostRequest("/Services/Update", slider);
            var result = request.ToDataResult<Service>();

            return result;
        }

        public IDataResult<Service> GetById(int sliderId)
        {
            var request = _httpHelper.GetRequest("/Services/getbyid/" + sliderId);
            var result = request.ToDataResult<Service>();

            return result;
        }
    }
}
