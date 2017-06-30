using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Infrastructure
{
    public class ValidateException : Exception
    {
        public int Code { get; set; }

        public ValidateException(int code, string msg) : base(msg)
        {
            this.Code = code;
        }
    }
}
