using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Utilities.Results;

namespace Web.Services.Abstract
{
    public interface IContactService
    {
        IDataResult<Contact> Update(Contact about);
        IDataResult<Contact> GetById(int id);
    }
}
