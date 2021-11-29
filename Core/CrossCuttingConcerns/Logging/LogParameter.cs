using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public string Name { get; set; } // instance adı ne verirsen  Product 
        public object Value { get; set; } // Id = 1, Name = acer
        public string Type { get; set; } //  Product

    }
}
