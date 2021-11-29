using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Utilities.HttpHelpers
{
    public interface IHttpHelper
    {
        public string BaseURL { get; set; }
        IRestResponse GetRequest(string resource, object parameters = null, bool removeToken = false);
        IRestResponse PostRequest(string resource, object parameters = null, bool removeToken = false);
    }
}
