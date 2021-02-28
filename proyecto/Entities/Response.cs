using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public bool TokenOK { get; set; }
    }
}
