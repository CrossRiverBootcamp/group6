using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.Services.Exceptions
{
    public class NotSavedException : Exception
    {
        public NotSavedException(string? message) : base(message)
        {
        }
    }
}
