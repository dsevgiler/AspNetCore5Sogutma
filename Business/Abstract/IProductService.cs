using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<ProductDto> GetById(int productId);
        IDataResult<Product> GetById2(int productId);
        IDataResult<List<ProductDto>> GetList();
        IDataResult<List<Product>> GetListByCategoryId(int Id);
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);

        IResult TransactionalOperation(Product product); // Test transaction
    }
}
