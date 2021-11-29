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
    public class ServiceManager : IServiceService
    {
        private readonly IServiceDal _serviceDal;

        public ServiceManager(IServiceDal serviceDal)
        {
            _serviceDal = serviceDal;
        }

        public IDataResult<Service> GetById(int serviceId)
        {
            var result = _serviceDal.Get(p => p.Id == serviceId);
            return result.ReturnDataResult();
        }

        public IDataResult<List<Service>> GetList()
        {
            var result = _serviceDal.GetList();
            return result.ReturnDataResult();
        }

        public IResult Add(Service service)
        {
            _serviceDal.Add(service);
            return new SuccessResult(Messages.SliderAdded);
        }

        public IResult Update(Service service)
        {
            _serviceDal.Update(service);
            return new SuccessResult(Messages.SliderUpdated);
        }

        public IResult Delete(Service service)
        {
            _serviceDal.Delete(service);
            return new SuccessResult(Messages.SliderDeleted);
        }

    }
}
