using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Abstract;
using Web.Utilities.Extentions;
using Web.Utilities.HttpHelpers;
using Web.Utilities.Results;

namespace Web.Services.Concrete
{
    public class ContactService : IContactService
    {
        private IHttpHelper _httpHelper;

        public ContactService(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public IDataResult<Contact> GetById(int id)
        {
            var request = _httpHelper.GetRequest("/Contacts/getbyid/" + id);
            var result = request.ToDataResult<Contact>();

            return result;
        }

        public IDataResult<Contact> Update(Contact contact)
        {
            var request = _httpHelper.PostRequest("/Contacts/Update", contact);
            var result = request.ToDataResult<Contact>();

            return result;
        }
    }
}
