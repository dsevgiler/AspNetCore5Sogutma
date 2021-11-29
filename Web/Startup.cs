using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System;
using Web.Services.Abstract;
using Web.Services.Concrete;
using Web.Utilities;
using Web.Utilities.DependencyResolvers;
using Web.Utilities.HttpHelpers;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings.ApiUrl = Configuration.GetSection("ApiUrl").Get<string>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = Schema.AuthenticationScheme;
            })
            .AddCookie(Schema.AuthenticationScheme, options =>
            {
                options.Cookie.Name = $"{Schema.Prefix}{Schema.AuthenticationCookie}";
                options.Cookie.HttpOnly = true; // cookie js tarafýndan çekilebilir olsun mu. true olunca çekilemez
                options.LoginPath = Schema.LoginPath;
                options.LogoutPath = Schema.LogoutPath;
                options.AccessDeniedPath = Schema.AccessDeniedPath;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.SameSite = SameSiteMode.Lax; // subdomain ve baþka sitelerde kullanýr. strict kullanamaz
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // 30 dakika tutsun
                options.SlidingExpiration = true;

            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation(); // refresh olduðunda sayfada projeyi restart yapmadan razor güncellensin. 

            services.AddSession(option => // token tutmak için kullandým. Login de
            {
                option.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            WebDependencyInjectionManager.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute();

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }

    }
}
