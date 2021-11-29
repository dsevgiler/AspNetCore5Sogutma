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
    public class CategoryService : ICategoryService
    {
        private readonly IHttpHelper _httpHelper;

        public CategoryService(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public IDataResult<Category> Add(Category category)
        {
            var request = _httpHelper.PostRequest("/Categories/Add", category);
            var result = request.ToDataResult<Category>();

            return result;
        }

        public IDataResult<Category> Delete(Category category)
        {
            var request = _httpHelper.PostRequest("/Categories/Delete", category);
            var result = request.ToDataResult<Category>();

            return result;
        }

        public IDataResult<List<Category>> GetList()
        {
            var request = _httpHelper.GetRequest("/Categories/getall");
            var result = request.ToDataResult<List<Category>>();

            return result;
        }

        public IDataResult<Category> Update(Category category)
        {
            var request = _httpHelper.PostRequest("/Categories/Update", category);
            var result = request.ToDataResult<Category>();

            return result;
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            var request = _httpHelper.GetRequest("/Categories/getbyid/" + categoryId);
            var result = request.ToDataResult<Category>();

            return result;
        }

        public List<SelectListItem> GetSelectList()
        {
            var request = _httpHelper.GetRequest("/Categories/getselectlist");

            var result = request.ToSelectList();
            return result.Data;
        }

    }
}
