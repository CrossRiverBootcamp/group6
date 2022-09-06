using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.Services.Exceptions
{
    internal class NotValidException : Exception
    {
        public NotValidException(string? message) : base(message)
        {
        }
    }
}
