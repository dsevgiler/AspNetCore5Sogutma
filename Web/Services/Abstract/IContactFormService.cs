using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface IContactFormService
    {
        IDataResult<List<ContactForm>> GetList();
        IDataResult<ContactForm> GetById(int id);
        IDataResult<ContactForm> Add(ContactForm mailForm);
        IDataResult<ContactForm> Update(ContactForm mailForm);
        IDataResult<ContactForm> Delete(ContactForm mailForm);
    }
}
