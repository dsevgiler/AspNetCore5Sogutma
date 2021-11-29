using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IContactFormService
    {
        IDataResult<ContactForm> GetById(int id);
        IDataResult<List<ContactForm>> GetList();
        IResult Add(ContactForm contactForm);
        IResult Delete(ContactForm contactForm);
        IResult Update(ContactForm contactForm);
    }
}
