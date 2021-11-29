using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors.Interceptors
{
    public abstract class MethodInterception:MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { } // Method çalışmadan önce çalışır
        protected virtual void OnAfter(IInvocation invocation) { } // Method çalıştıktan sonra çalışır 
        protected virtual void OnExeption(IInvocation invocation, Exception e) { } // Method hata verdiğinde çalışır 
        protected virtual void OnSuccess(IInvocation invocation) { } // Method başarılıysa çalışır

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;

            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnExeption(invocation, e);

                new ExceptionLogAspect(typeof(DatabaseLogger));

                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }

            OnAfter(invocation);

        }
    }
}
