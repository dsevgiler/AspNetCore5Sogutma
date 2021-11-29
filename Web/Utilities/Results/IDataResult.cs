using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Utilities.Results
{
    public interface IDataResult<T>
    {
        T Data { get; set; }
        bool Success { get; set; }
        int StatusCode { get; set; }
        string StatusDescription { get; set; }
        string Message { get; set; }

    }
}
