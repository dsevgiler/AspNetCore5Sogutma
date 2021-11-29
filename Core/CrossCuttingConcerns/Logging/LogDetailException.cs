using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetailException : LogDetail
    {
        public string ExceptionMessage { get; set; }
    }
}
