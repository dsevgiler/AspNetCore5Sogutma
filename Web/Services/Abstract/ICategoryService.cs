using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetList();
        IDataResult<Category> GetById(int categoryId);
        IDataResult<Category> Add(Category category);
        IDataResult<Category> Update(Category category);
        IDataResult<Category> Delete(Category category);

        List<SelectListItem> GetSelectList();

    }
}
