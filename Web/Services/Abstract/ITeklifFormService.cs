

using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface ITeklifFormService
    {
        IDataResult<List<TeklifForm>> GetList();
        IDataResult<TeklifForm> GetById(int id);
        IDataResult<TeklifForm> Add(TeklifForm teklifForm);
        IDataResult<TeklifForm> Update(TeklifForm teklifForm);
        IDataResult<TeklifForm> Delete(TeklifForm teklifForm);
    }
}
