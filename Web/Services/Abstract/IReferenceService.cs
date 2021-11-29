using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface IReferenceService
    {
        IDataResult<List<Reference>> GetList();
        IDataResult<Reference> GetById(int id);
        IDataResult<Reference> Add(Reference reference);
        IDataResult<Reference> Update(Reference reference);
        IDataResult<Reference> Delete(Reference reference);
    }
}
