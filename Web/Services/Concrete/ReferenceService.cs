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
    public class ReferenceService : IReferenceService
    {
        private readonly IHttpHelper _httpHelper;

        public ReferenceService(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public IDataResult<Reference> Add(Reference reference)
        {
            var request = _httpHelper.PostRequest("/References/Add", reference);
            var result = request.ToDataResult<Reference>();

            return result;
        }

        public IDataResult<Reference> Delete(Reference reference)
        {
            var request = _httpHelper.PostRequest("/References/Delete", reference);
            var result = request.ToDataResult<Reference>();

            return result;
        }

        public IDataResult<Reference> GetById(int id)
        {
            var request = _httpHelper.GetRequest("/References/getbyid/" + id);
            var result = request.ToDataResult<Reference>();

            return result;
        }

        public IDataResult<List<Reference>> GetList()
        {
            var request = _httpHelper.GetRequest("/References/getall");
            var result = request.ToDataResult<List<Reference>>();

            return result;
        }

        public IDataResult<Reference> Update(Reference reference)
        {
            var request = _httpHelper.PostRequest("/References/Update", reference);
            var result = request.ToDataResult<Reference>();

            return result;
        }
    }
}
