using Entities.Concrete;
using Entities.Dtos;
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
    public class ProductService : IProductService
    {
        private readonly IHttpHelper _httpHelper;

        public ProductService(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public IDataResult<ProductDto> GetById(int id)
        {
            var request = _httpHelper.GetRequest("/Products/getbyid/" + id);
            var result = request.ToDataResult<ProductDto>();

            return result;
        }

        public IDataResult<Product> GetById2(int id)
        {
            var request = _httpHelper.GetRequest("/Products/getbyid2/" + id);
            var result = request.ToDataResult<Product>();

            return result;
        }

        public IDataResult<ProductDto> Add(ProductDto product)
        {
            var request = _httpHelper.PostRequest("/Products/add", product);
            var result = request.ToDataResult<ProductDto>();

            return result;
        }

        public IDataResult<Product> Delete(Product product)
        {
            var request = _httpHelper.PostRequest("/Products/Delete", product);
            var result = request.ToDataResult<Product>();

            return result;
        }

        public IDataResult<List<ProductDto>> GetList()
        {
            var request = _httpHelper.GetRequest("/Products/getall");
            var result = request.ToDataResult<List<ProductDto>>();

            return result;
        }

        public IDataResult<Product> Update(Product product)
        {
            var request = _httpHelper.PostRequest("/Products/Update", product);
            var result = request.ToDataResult<Product>();

            return result;
        }
    }
}
