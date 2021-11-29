using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(inherit: true).ToList();

            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(inherit: true);

            classAttributes.AddRange(methodAttributes);
            
            //classAttributes.Add(new LogAspect(typeof(DatabaseLogger)));  // FileLogger da olabilirler. 

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
