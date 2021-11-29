using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModul : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<ReferenceManager>().As<IReferenceService>();
            builder.RegisterType<EfReferenceDal>().As<IReferenceDal>();

            builder.RegisterType<SliderManager>().As<ISliderService>();
            builder.RegisterType<EfSliderDal>().As<ISliderDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<ServiceManager>().As<IServiceService>();
            builder.RegisterType<EfServiceDal>().As<IServiceDal>();

            builder.RegisterType<AboutManager>().As<IAboutService>();
            builder.RegisterType<EfAboutDal>().As<IAboutDal>();

            builder.RegisterType<ContactManager>().As<IContactService>();
            builder.RegisterType<EfContactDal>().As<IContactDal>();

            builder.RegisterType<ContactFormManager>().As<IContactFormService>();
            builder.RegisterType<EfContactFormDal>().As<IContactFormDal>();

            builder.RegisterType<TeklifFormManager>().As<ITeklifFormService>();
            builder.RegisterType<EfTeklifFormDal>().As<ITeklifFormDal>();


            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); //Mevcut assembly'e ulaştım.

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
