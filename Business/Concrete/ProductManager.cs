using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Trasnsaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.FluentValidation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Messages;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ITokenHelper _tokenHelper;

        public ProductManager(IProductDal productDal, ITokenHelper tokenHelper)
        {
            _productDal = productDal;
            _tokenHelper = tokenHelper;
        }

        [ValidationAspect(typeof(ProductValidator), Priority = 1)] 
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.Name));
            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded); 
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<ProductDto> GetById(int productId)
        {
            var result = _productDal.GetByIdWithCategory(productId);
            return result.ReturnDataResult();
        }

        public IDataResult<Product> GetById2(int productId)
        {
            var result = _productDal.Get(x=>x.Id == productId);
            return result.ReturnDataResult();
        }

        //[SecuredOperation("Product.List, Admin")] // yetkisi iki rolden biri olmalı. sıra önemli önce secured olmalı yetki yoksa cache olmamalı
        //[CacheAspect(10)]// 10 dakika cache le
        //[CacheRemoveAspect("IProductService.GetList")] cache i kaldır
        //[PerformanceAspect(5)]  // eğer 5 sn den fazla olursa output a yazacak.
        public IDataResult<List<ProductDto>> GetList()
        {
            //var username = _tokenHelper.GetEmail();
            var result = _productDal.GetListWithCategory();
            return result.ReturnDataResult();
        }

        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Product>> GetListByCategoryId(int Id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == Id).ToList());
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductUpdated);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }


        #region Rules
        private IResult CheckIfProductNameExists(string productName)
        {
            if (_productDal.Get(p => p.Name == productName) != null)
            {
                return new ErrorResult("Ürün ismi zaten mevcut");
            }

            return new SuccessResult();
        } 
        #endregion
    }
}
