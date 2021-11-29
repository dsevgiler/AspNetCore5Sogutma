using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Utilities.HttpHelpers
{
    public class HttpHelper : IHttpHelper
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IRestClient _restClient;
        private string _baseUrl;

        public HttpHelper(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor, IRestClient restClient, string baseUrl)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(baseUrl);
            _baseUrl = baseUrl;
        }

        public string BaseURL
        {
            get { return _baseUrl; }
            set 
            {
                _baseUrl = value;
                _restClient = new RestClient(value);
            }
        }

        private void AddParameterListForRequest(RestRequest request, JObject keyValuePairs, ParameterType parameterType, string contentType = "")
        {
            if (keyValuePairs != null)
                if (string.IsNullOrEmpty(contentType))
                    foreach (var keyValuePair in keyValuePairs)
                        request.AddParameter(new Parameter(keyValuePair.Key, keyValuePair.Value, parameterType));
                else
                    foreach (var keyValuePair in keyValuePairs)
                        request.AddParameter(new Parameter(keyValuePair.Key, keyValuePair.Value, contentType, parameterType));
        }

        private void AddTokenToRequest(RestRequest request)
        {
            request.AddHeader("Authorization", "Bearer " + _serviceProvider.GetRequiredService<IWebContext>().JwtAccessToken.Token);
        }

        public IRestResponse GetRequest(string resource, object parameters = null, bool removeToken = false)
        {
            _restClient.Timeout = -1;
            var request = new RestRequest(resource, Method.GET);
            request.AddHeader("Content-Type", "application/json");

            if (parameters != null)
            {
                var _params = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(parameters));
                AddParameterListForRequest(request, _params, ParameterType.QueryString);
            }

            if (!removeToken)
                AddTokenToRequest(request);

            IRestResponse response = _restClient.Execute(request);

            return response;
        }

        public IRestResponse PostRequest(string resource, object parameters = null, bool removeToken = false)
        {
            _restClient.Timeout = -1;
            var request = new RestRequest(resource, Method.POST);
            request.AddHeader("Content-Type", "application/json");

            if (parameters != null)
                request.AddJsonBody(parameters);

            if (!removeToken)
                AddTokenToRequest(request);

            IRestResponse response = _restClient.Execute(request);

            return response;
        }
    }
}
