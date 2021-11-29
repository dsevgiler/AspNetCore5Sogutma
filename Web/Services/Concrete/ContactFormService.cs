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
    public class ContactFormService : IContactFormService
    {
        private readonly IHttpHelper _httpHelper;

        public ContactFormService(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public IDataResult<ContactForm> Add(ContactForm contact)
        {
            var request = _httpHelper.PostRequest("/ContactForms/Add", contact);
            var result = request.ToDataResult<ContactForm>();

            return result;
        }

        public IDataResult<ContactForm> Delete(ContactForm reference)
        {
            var request = _httpHelper.PostRequest("/ContactForms/Delete", reference);
            var result = request.ToDataResult<ContactForm>();

            return result;
        }

        public IDataResult<ContactForm> GetById(int id)
        {
            var request = _httpHelper.GetRequest("/ContactForms/getbyid/" + id);
            var result = request.ToDataResult<ContactForm>();

            return result;
        }

        public IDataResult<List<ContactForm>> GetList()
        {
            var request = _httpHelper.GetRequest("/ContactForms/getall");
            var result = request.ToDataResult<List<ContactForm>>();

            return result;
        }

        public IDataResult<ContactForm> Update(ContactForm reference)
        {
            var request = _httpHelper.PostRequest("/ContactForms/Update", reference);
            var result = request.ToDataResult<ContactForm>();

            return result;
        }
    }
}
