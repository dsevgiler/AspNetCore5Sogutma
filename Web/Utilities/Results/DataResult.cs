using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Utilities.Results
{
    public class DataResult<T> : IDataResult<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Message { get; set; }
    }
}
