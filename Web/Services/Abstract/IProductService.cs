using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface IProductService
    {
        IDataResult<List<ProductDto>> GetList();
        IDataResult<ProductDto> GetById(int id);
        IDataResult<Product> GetById2(int id);
        IDataResult<ProductDto> Add(ProductDto product);
        IDataResult<Product> Update(Product product);
        IDataResult<Product> Delete(Product product);

    }
}
