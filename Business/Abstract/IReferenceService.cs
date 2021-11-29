using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IReferenceService
    {
        IDataResult<Reference> GetById(int referenceId);
        IDataResult<List<Reference>> GetList();
        IResult Add(Reference reference);
        IResult Delete(Reference reference);
        IResult Update(Reference reference);
    }
}
