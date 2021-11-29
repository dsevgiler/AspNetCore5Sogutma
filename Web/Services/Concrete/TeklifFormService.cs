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
    public class TeklifFormService : ITeklifFormService
    {
        private readonly IHttpHelper _httpHelper;

        public TeklifFormService(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public IDataResult<TeklifForm> Add(TeklifForm teklifForm)
        {
            var request = _httpHelper.PostRequest("/TeklifForms/Add", teklifForm);
            var result = request.ToDataResult<TeklifForm>();

            return result;
        }

        public IDataResult<TeklifForm> Delete(TeklifForm teklifForm)
        {
            var request = _httpHelper.PostRequest("/TeklifForms/Delete", teklifForm);
            var result = request.ToDataResult<TeklifForm>();

            return result;
        }

        public IDataResult<TeklifForm> GetById(int id)
        {
            var request = _httpHelper.GetRequest("/TeklifForms/getbyid/" + id);
            var result = request.ToDataResult<TeklifForm>();

            return result;
        }

        public IDataResult<List<TeklifForm>> GetList()
        {
            var request = _httpHelper.GetRequest("/TeklifForms/getall");
            var result = request.ToDataResult<List<TeklifForm>>();

            return result;
        }

        public IDataResult<TeklifForm> Update(TeklifForm teklifForm)
        {
            var request = _httpHelper.PostRequest("/TeklifForm/Update", teklifForm);
            var result = request.ToDataResult<TeklifForm>();

            return result;
        }
    }
}
