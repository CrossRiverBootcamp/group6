using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class NotSavedException : Exception
    {
        public NotSavedException(string? message) : base(message)
        {
        }
    }
}
