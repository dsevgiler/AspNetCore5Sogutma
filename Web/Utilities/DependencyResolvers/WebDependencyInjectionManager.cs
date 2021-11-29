using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Abstract;
using Web.Services.Concrete;
using Web.Utilities.HttpHelpers;

namespace Web.Utilities.DependencyResolvers
{
    public static class WebDependencyInjectionManager
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReferenceService, ReferenceService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactFormService, ContactFormService>();
            services.AddScoped<ITeklifFormService, TeklifFormService>();


            services.AddScoped<ICookieAuthenticationService, CookieAuthenticationService>();
            services.AddScoped<INotification, Notification>();
            services.AddScoped<IRestClient, RestClient>();

            services.AddScoped<IHttpHelper>(helper =>
            new HttpHelper(
                helper.GetService<IServiceProvider>(),
                helper.GetService<IHttpContextAccessor>(),
                helper.GetService<IRestClient>(),
                AppSettings.ApiUrl)
            );

            services.AddScoped<IWebContext, WebContext>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        }

    }
}
