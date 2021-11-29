using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Web.Services.Abstract;
using Web.Utilities.Results;

namespace Web.Utilities.Extentions
{
    public static class ConverterExtensions
    {
        public static IDataResult<T> ToDataResult<T>(this IRestResponse response) where T : class
        {
            IDataResult<T> _responseData;

            try
            {
                if ((response.StatusCode == HttpStatusCode.BadRequest && response.Content.Contains("errors")))
                {
                    var badRequestData = JsonConvert.DeserializeObject<JsonObject>(response.Content);
                    string error_message = "";
                    try
                    {
                        error_message = ((Newtonsoft.Json.Linq.JValue)((Newtonsoft.Json.Linq.JProperty)((Newtonsoft.Json.Linq.JObject)badRequestData["errors"]).First).Single().First()).Value.ToString();
                    }
                    catch { }

                    _responseData = new DataResult<T>()
                    {
                        Message = (
                            response.StatusDescription + ", " +
                            (
                                badRequestData.ContainsKey("title") ?
                                badRequestData["title"].ToString() + "," + error_message
                                : response.Content
                            ) + (response.ErrorMessage == null ? "" : response.ErrorMessage)
                        ),
                        Success = false
                    };
                }
                else
                {
                    if (response.Content.Length > 0)
                    {
                        _responseData = JsonConvert.DeserializeObject<DataResult<T>>(response.Content);
                    }
                    else
                    {
                        _responseData = new DataResult<T>()
                        {
                            Message = response.StatusDescription + ", " + response.Content + ", " + response.ErrorMessage,
                            Success = false
                        };
                    }
                }
            }
            catch
            {
                _responseData = new DataResult<T>()
                {
                    Message = response.StatusDescription + ", " + response.Content + ", " + response.ErrorMessage,
                    Success = false
                };
            }

            _responseData.StatusCode = Convert.ToInt32(response.StatusCode);
            _responseData.StatusDescription = response.StatusDescription;

            return _responseData;
        }

        public static IDataResult<T> ToDataResult<T>(this IRestResponse response, INotification notification) where T : class
        {

            IDataResult<T> _responseData;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<DataResult<T>>(response.Content);

                if (result.Success)
                {
                    if (!string.IsNullOrEmpty(result.Message))
                        notification.Success(result.Message);

                    _responseData = result;
                }
                else
                {
                    if (!string.IsNullOrEmpty(result.Message))
                    {
                        notification.Error(result.Message);
                        _responseData = new DataResult<T>() { Success = false, Message = result.Message, Data = null };
                    }
                    else
                    {
                        _responseData = new DataResult<T>() { Success = false, Message = "Başarısız", Data = null };
                    }
                }
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                notification.Error("Yetkiniz Yok");
                _responseData = new DataResult<T>() { Success = false, Message = "Yetkiniz Yok", Data = null };
            }
            else
            {
                //notification.Error("Başarısız");
                _responseData = new DataResult<T>() { Success = false, Message = response.Content, Data = null };
            }



            _responseData.StatusCode = Convert.ToInt32(response.StatusCode);
            _responseData.StatusDescription = response.StatusDescription;
            return _responseData;
        }

        public static IDataResult<List<SelectListItem>> ToSelectList(this IRestResponse response, bool addDefault = false, SelectListItem defaultItem = null)
        {

            IDataResult<List<SelectListItem>> _responseData;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<DataResult<List<SelectListItem>>>(response.Content);

                if (result.Success)
                {
                    if (addDefault)
                    {
                        if (defaultItem != null)
                        {
                            defaultItem.Selected = true;
                            result.Data.Insert(0, defaultItem);
                        }
                        else
                        {

                            result.Data.Insert(0, new SelectListItem { Text = "Seçiniz", Value = "0" });
                        }

                    }


                    _responseData = result;
                }
                else
                {
                    if (!string.IsNullOrEmpty(result.Message))
                    {

                        _responseData = new DataResult<List<SelectListItem>>() { Success = false, Message = result.Message, Data = new List<SelectListItem>() };
                    }
                    else
                    {
                        _responseData = new DataResult<List<SelectListItem>>() { Success = false, Message = "Başarısız", Data = new List<SelectListItem>() { new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true } } };
                    }
                }
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                _responseData = new DataResult<List<SelectListItem>>() { Success = false, Message = "Yetkiniz Yok", Data = new List<SelectListItem>() { new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true } } };
            }
            else
            {

                _responseData = new DataResult<List<SelectListItem>>() { Success = false, Message = "Başarısız", Data = new List<SelectListItem>() { new SelectListItem { Text = "Seçiniz", Value = "0", Selected = true } } };
            }

            _responseData.StatusCode = Convert.ToInt32(response.StatusCode);
            _responseData.StatusDescription = response.StatusDescription;
            return _responseData;

        }
    }
}
