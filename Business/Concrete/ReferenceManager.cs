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
    public class ReferenceManager : IReferenceService
    {
        private readonly IReferenceDal _referenceDal;

        public ReferenceManager(IReferenceDal referenceDal)
        {
            _referenceDal = referenceDal;
        }

        public IResult Add(Reference reference)
        {
            IResult result = BusinessRules.Run(CheckIfReferenceNameExists(reference.CompanyName));
            if (result != null)
            {
                return result;
            }

            _referenceDal.Add(reference);
            return new SuccessResult(Messages.ReferenceAdded);
        }

        public IResult Delete(Reference reference)
        {
            _referenceDal.Delete(reference);
            return new SuccessResult(Messages.ReferenceDeleted);
        }

        public IDataResult<Reference> GetById(int id)
        {
            var result = _referenceDal.Get(p => p.Id == id);
            return result.ReturnDataResult();
        }

        public IDataResult<List<Reference>> GetList()
        {
            var result = _referenceDal.GetList();
            return result.ReturnDataResult();
        }

        public IResult Update(Reference reference)
        {
            _referenceDal.Update(reference);
            return new SuccessResult(Messages.ReferenceUpdated);
        }

        #region Rules
        private IResult CheckIfReferenceNameExists(string referenceName)
        {
            if (_referenceDal.Get(p => p.CompanyName == referenceName) != null)
            {
                return new ErrorResult(Messages.ReferenceAlreadyExists);
            }

            return new SuccessResult();
        }

        #endregion
    }
}
