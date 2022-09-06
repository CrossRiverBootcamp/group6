using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.Services.Exceptions
{
    public class NoAccessException : Exception
    {
        public NoAccessException(string? message) : base(message)
        {
        }
    }
}
