using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;

        public ExceptionLogAspect(Type loggerServiceBase)
        {
            if (loggerServiceBase.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(Utilities.Messages.Message.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerServiceBase);
        }

        protected override void OnExeption(IInvocation invocation, System.Exception e)
        {
            LogDetailException logDetailException = GetLogDetail(invocation);
            logDetailException.ExceptionMessage = e.Message;
            _loggerServiceBase.Error(logDetailException);
        }

        private LogDetailException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name

                });
            }

            var logDetailException = new LogDetailException
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters

            };

            return logDetailException;

        }
    }
}
