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
    public class ContactFormManager : IContactFormService
    {
        private readonly IContactFormDal _contactFormDal;

        public ContactFormManager(IContactFormDal contactFormDal)
        {
            _contactFormDal = contactFormDal;
        }

        public IResult Add(ContactForm contactForm)
        {
            _contactFormDal.Add(contactForm);
            return new SuccessResult(Messages.FormAdded);
        }

        public IResult Delete(ContactForm reference)
        {
            _contactFormDal.Delete(reference);
            return new SuccessResult(Messages.FormDeleted);
        }

        public IDataResult<ContactForm> GetById(int id)
        {
            var result = _contactFormDal.Get(p => p.Id == id);
            return result.ReturnDataResult();
        }

        public IDataResult<List<ContactForm>> GetList()
        {
            var result = _contactFormDal.GetList();
            return result.ReturnDataResult();
        }

        public IResult Update(ContactForm contactForm)
        {
            _contactFormDal.Update(contactForm);
            return new SuccessResult(Messages.FormUpdated);
        }

    }
}
