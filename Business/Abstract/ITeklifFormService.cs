using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITeklifFormService
    {
        IDataResult<TeklifForm> GetById(int id);
        IDataResult<List<TeklifForm>> GetList();
        IResult Add(TeklifForm teklifForm);
        IResult Delete(TeklifForm teklifForm);
        IResult Update(TeklifForm teklifForm);
    }
}
