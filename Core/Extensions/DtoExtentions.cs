using Core.Utilities.Messages;
using Core.Utilities.Results;
using System;
using System.Collections;
using System.Text;

namespace Core.Extensions
{
    public static class DtoExtensions
    {
        public static IDataResult<T> ReturnDataResult<T>(this T result) where T : class, new()
        {
            if (result != null)
            {

                if (result is IList && result.GetType().IsGenericType)
                {

                    if (((IList)result).Count == 0)
                    {
                        return new SuccessDataResult<T>(result, Message.RecordNotFound);
                    }
                    else
                    {
                        return new SuccessDataResult<T>(result, Message.Success);
                    }
                }
                else
                {
                    if (result == default(T))
                    {
                        return new SuccessDataResult<T>(result, Message.RecordNotFound);
                    }
                    else
                    {
                        return new SuccessDataResult<T>(result, Message.Success);
                    }

                }
            }
            else
            {
                return new ErrorDataResult<T>(result, Message.Error);
            }
        }

    }
}
