using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Abstract;

namespace Web.Services.Concrete
{
    public class Notification : INotification
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        private ITempDataDictionary _tempData;

        public Notification(
            IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;

            _tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);
        }



        public virtual void Success(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Success, message, persistForTheNextRequest);
        }

        public virtual void Warning(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Warning, message, persistForTheNextRequest);
        }

        public virtual void Error(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, message, persistForTheNextRequest);
        }

        public virtual void Error(Exception exception, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, exception.Message, persistForTheNextRequest);
        }

        public virtual void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
        {
            var dataKey = $"ucsan.notifications.{type}";

            if (persistForTheNextRequest)
            {
                if (_tempData[dataKey] == null || !(_tempData[dataKey] is List<string>))
                    _tempData[dataKey] = new List<string>();
                ((List<string>)_tempData[dataKey]).Add(message);
            }
            else
            {
                if (_httpContextAccessor.HttpContext.Items[dataKey] == null || !(_httpContextAccessor.HttpContext.Items[dataKey] is List<string>))
                    _httpContextAccessor.HttpContext.Items[dataKey] = new List<string>();
                ((List<string>)_httpContextAccessor.HttpContext.Items[dataKey]).Add(message);
            }
        }
    }
}
