using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class TeklifFormManager : ITeklifFormService
    {
        private readonly ITeklifFormDal _teklifFormDal;

        public TeklifFormManager(ITeklifFormDal teklifFormDal)
        {
            _teklifFormDal = teklifFormDal;
        }

        public IResult Add(TeklifForm teklifForm)
        {
            _teklifFormDal.Add(teklifForm);
            return new SuccessResult(Messages.FormAdded);
        }

        public IResult Delete(TeklifForm teklifForm)
        {
            _teklifFormDal.Delete(teklifForm);
            return new SuccessResult(Messages.FormDeleted);
        }

        public IDataResult<TeklifForm> GetById(int id)
        {
            var result = _teklifFormDal.Get(p => p.Id == id);
            return result.ReturnDataResult();
        }

        public IDataResult<List<TeklifForm>> GetList()
        {
            var result = _teklifFormDal.GetList();
            return result.ReturnDataResult();
        }

        public IResult Update(TeklifForm teklifForm)
        {
            _teklifFormDal.Update(teklifForm);
            return new SuccessResult(Messages.FormUpdated);
        }

    }
}
