using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface IServiceService
    {
        IDataResult<List<Service>> GetList();
        IDataResult<Service> GetById(int id);
        IDataResult<Service> Add(Service slider);
        IDataResult<Service> Update(Service slider);
        IDataResult<Service> Delete(Service slider);
    }
}
