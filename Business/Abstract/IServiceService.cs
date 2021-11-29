using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IServiceService
    {
        IDataResult<Service> GetById(int serviceId);
        IDataResult<List<Service>> GetList();
        IResult Add(Service service);
        IResult Delete(Service service);
        IResult Update(Service service);
    }
}
