using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            var result = _categoryDal.Get(p => p.Id == categoryId);
            return result.ReturnDataResult();
        }

        public IDataResult<List<Category>> GetList()
        {
            var result = _categoryDal.GetList();
            return result.ReturnDataResult();
        }

        public IResult Add(Category category)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(category.Name));
            if (result != null)
            {
                return result;
            }

            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IDataResult<List<SelectListItemDto>> GetSelectList()
        {
            var result = _categoryDal.GetList()
            .OrderBy(F => F.Name)
                .Select(F => new SelectListItemDto { Value = F.Id.ToString(), Text = string.Format("{0} - {1}", F.Id, F.Name) })
            .ToList();

            return result.ReturnDataResult();
        }

        #region Rules
        private IResult CheckIfProductNameExists(string categoryName)
        {
            if (_categoryDal.Get(p => p.Name == categoryName) != null)
            {
                return new ErrorResult(Messages.CategoryAlreadyExist);
            }

            return new SuccessResult();
        }

        #endregion
    }
}
