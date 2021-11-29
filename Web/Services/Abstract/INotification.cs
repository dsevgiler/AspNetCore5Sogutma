using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Concrete;

namespace Web.Services.Abstract
{
    public interface INotification
    {
        void Success(string message, bool persistForTheNextRequest = true);
        void Warning(string message, bool persistForTheNextRequest = true);
        void Error(string message, bool persistForTheNextRequest = true);
        void Error(Exception exception, bool persistForTheNextRequest = true);
        void AddNotification(NotifyType type, string message, bool persistForTheNextRequest);
    }
}
